﻿using SolrExpress.Core.Exception;
using SolrExpress.Core.Query;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace SolrExpress.Solr4
{
    /// <summary>
    /// SOLR 4.9x access provider
    /// </summary>
    public class Provider : IProvider
    {
        private readonly string _solrHost;

        public string SearchHandlerName { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="solrHost">Solr host address</param>
        public Provider(string solrHost, string searchUrl = "query")
        {
            this._solrHost = solrHost;
            SearchHandlerName = searchUrl;
        }

        /// <summary>
        /// Execute the parameters and return the formed solr query
        /// </summary>
        /// <param name="parameters">List of the parameters arranged in the queryable class</param>
        /// <returns>Solr query</returns>
        public string GetQuery(List<IParameter> parameters)
        {
            var list = new List<string>();

            foreach (var item in parameters.OrderBy(q => q.GetType().ToString()))
            {
                ((IParameter<List<string>>)item).Execute(list);
            }

            return $"{SearchHandlerName}?{string.Join("&", list)}";
        }

        /// <summary>
        /// Execute the informated uri and return the result of the request
        /// </summary>
        /// <param name="uri">Solr query uri</param>
        /// <returns>Result of the request</returns>
        public string Execute(string query)
        {
            var baseUrl = $"{this._solrHost}/{query}&echoParams=none&wt=json&indent=off";

            var request = WebRequest.Create(baseUrl);
            request.Method = "GET";

            try
            {
                var response = (HttpWebResponse)request.GetResponse();

                string content;

                using (var dataStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(dataStream))
                    {
                        content = reader.ReadToEnd();
                    }
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new UnexpectedJsonQueryException(content);
                }

                return content;
            }
            catch (System.Exception e)
            {
                throw new UnexpectedJsonQueryException(e.Message);
            }
        }
    }
}
