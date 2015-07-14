# MvcMusicStore2013 (updated) - Extending ASP.NET MVC Music Store with Elasticsearch
Add Elasticsearch to the popular MvcMusicStore example project (converted to MVC4).

Description: ElasticSearch is a great and powerful open source search engine that can be used to solve a great range of problems. Here we'll take a look at how we can use ElasticSearch in an ASP.NET MVC application.

Original Author: Joel Abrahamsson

Original source for article: [Original link][1] / [Archived link][2]

ElasticSearch/NEST Search Documentation: [link][3]

ElasticSearch.Net & NEST Documentation (.NET): [link][5]

INSTRUCTIONS
------------

1. Install ElasticSearch.  For Windows, simply download the [ZIP file][4], extract it to your directory of choice, and install.  The Java JDK/JRE is required and the **'JAVA_HOME'** system variable must be set.  Once this is confirmed, navigate to the directory and run "c:\elasticsearch-1.6.0\elasticsearch.bat".
   Once it's running, the ElasticSearch console will look something like this: ![ES Console](http://i.imgur.com/il9OwKn.jpg)
2. To view the ES data, navigate to [http://localhost:9200/_search?q=*][6].
3. In order for this example to run, the data must first be indexed by ES.  To do this, navigate to '~/storemanager/reindex'.  This will create an index called 'musicstore' containing album information.  In this example, ES automatically infers the Id of the album.
4. To see the results after indexing, simply navigate to '[http://localhost:36000/store/search?q=deep%20purple][7]'.
5. Starting, stopping, and restarting an ES cluster: [link][11]

> NOTE: This is working as of 7/14/2015.  Changes in references/DLL's (including ElasticSearch) and/or NuGet packages may prevent this solution from compiling.

ADDITIONAL LINKS
----------------

1. [Elasticsearch Service Wrapper][8] (uses Java Service Wrapper) for additional control.  This allows you to run ES as a daemon or inside the console and makes it easier to start & stop the service.
2. [Elasticsearch-head][9] is a free web browsing front end for browsing & interacting with an ES cluster.
3. [Marvel/Sense][10] is a commercial web browsing front-end that also has performance & diagnostic info.

[1]: http://joelabrahamsson.com/extending-aspnet-mvc-music-store-with-elasticsearch/
[2]: https://archive.is/y8eMv
[3]: https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/_nest.html
[4]: https://download.elastic.co/elasticsearch/elasticsearch/elasticsearch-1.6.0.zip
[5]: https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/index.html
[6]: http://localhost:9200/_search?q=*
[7]: http://localhost:36000/store/search?q=deep%20purple
[8]: https://github.com/elastic/elasticsearch-servicewrapper
[9]: http://mobz.github.io/elasticsearch-head/
[10]: https://www.elastic.co/downloads/marvel
[11]: https://library.osu.edu/blogs/it/elasticsearch-start-stop-restart/
