using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

using Nest;

namespace MvcMusicStore.Helpers
{
    public class ElasticSearchHelper
    {
        public static ElasticClient ElasticClient
        {
            get
            {
                var node = new Uri(ConfigurationManager.AppSettings["ElasticSearchUrl"] ?? "http://localhost:9200");
                var settings = new ConnectionSettings(node);
                settings.SetDefaultIndex(ConfigurationManager.AppSettings["ESDefaultIndex"] ?? "musicstore");
                return new ElasticClient(settings);
            }
        }
    }
}