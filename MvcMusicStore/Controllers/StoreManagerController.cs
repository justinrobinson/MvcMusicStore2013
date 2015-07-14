using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using MvcMusicStore.Models;
using Nest;
using System.Configuration;

namespace MvcMusicStore.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        private MusicStoreEntities db = new MusicStoreEntities();

        /// <summary>
        /// (re-)Index Albums
        /// </summary>
        /// <referenceUrl>https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/_nest.html</referenceUrl>
        /// <returns></returns>
        public ActionResult ReIndex()
        {
            var node = new Uri(ConfigurationManager.AppSettings["ElasticSearchUrl"] ?? "http://localhost:9200");
            var esIndex = ConfigurationManager.AppSettings["ESDefaultIndex"] ?? "musicstore";
            //var settings = new ConnectionSettings("localhost", 9200);            
            //var settings = new ConnectionSettings(node, defaultIndex: esIndex);
            var settings = new ConnectionSettings(node);                        
            settings.SetDefaultIndex(esIndex);
            var client = new ElasticClient(settings);

            foreach (var album in db.Albums)
            {
                //client.Index(album, "musicstore", "albums", album.AlbumId);   // outdated
                //client.Index(album, i => i.Id(album.AlbumId))
                client.Index(album);    // ES will infer the
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /StoreManager/

        public ViewResult Index()
        {
            var albums = db.Albums.Include(a => a.Genre).Include(a => a.Artist);
            return View(albums.ToList());
        }

        //
        // GET: /StoreManager/Details/5

        public ViewResult Details(int id)
        {
            Album album = db.Albums.Find(id);
            return View(album);
        }

        //
        // GET: /StoreManager/Create

        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
            return View();
        } 

        //
        // POST: /StoreManager/Create

        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }
        
        //
        // GET: /StoreManager/Edit/5
 
        public ActionResult Edit(int id)
        {
            Album album = db.Albums.Find(id);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        //
        // GET: /StoreManager/Delete/5
 
        public ActionResult Delete(int id)
        {
            Album album = db.Albums.Find(id);
            return View(album);
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}