using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MvcMusicStore.Helpers;
using MvcMusicStore.Models;

using Nest;

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        MusicStoreEntities storeDB = new MusicStoreEntities();

        //
        // GET: /Store/Search?genre=Disco
        /// <summary>
        /// Basic ElasticSearch search, for genres
        /// </summary>
        /// <referenceUrl>https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/_nest.html</referenceUrl>
        /// <param name="q">search query string</param>
        /// <returns></returns>
        public ActionResult Search(string q)
        {
            /* basic ElasticSearch query
             * use the Search method specifying what type of search for using it's type parameter.  
             * A delegate modifies the search request body (could also filter, request facets, number of hits, etc.)
             */
            var client = ElasticSearchHelper.ElasticClient;
            //// uses /other-index/other-type/_search
            //var searchResultsExample1 = client.Search<Album>(s => s.Index("other-index").OtherType("other-type"));
            //// uses /_all/person/_search
            //var searchResultsExample2 = client.Search<Album>(s => s.AllIndices());
            //// uses /_search
            //var searchResultsExample3 = client.Search<Album>(s => s.AllIndices().AllTypes());

            //var result = ElasticClient.Search<Album>(body =>
            //       body.Query(query =>
            //       query.QueryString(qs => qs.Query(q))));
            var result = client.Search<Album>(
                    s => s.From(0)
                        .Size(50)   // grab first 50 results
                        .Query(query => query.QueryString(qs => qs.Query(q))));

            var genre = new Genre()
                            {
                                Name = "Search results for " + q, 
                                Albums = result.Documents.ToList()
                            };

            return View("Browse", genre);
        }

        //
        // GET: /Store/

        public ActionResult Index()
        {
            var genres = storeDB.Genres.ToList();

            return View(genres);
        }

        //
        // GET: /Store/Browse?genre=Disco

        public ActionResult Browse(string genre)
        {
            // Retrieve Genre and its Associated Albums from database
            var genreModel = storeDB.Genres.Include("Albums")
                .Single(g => g.Name == genre);

            return View(genreModel);
        }

        //
        // GET: /Store/Details/5

        public ActionResult Details(int id)
        {
            var album = storeDB.Albums.Find(id);

            return View(album);
        }

        //
        // GET: /Store/GenreMenu

        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = storeDB.Genres.ToList();

            return PartialView(genres);
        }

    }
}