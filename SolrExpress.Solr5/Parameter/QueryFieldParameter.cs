﻿using Newtonsoft.Json.Linq;
using SolrExpress.Core.Query;

namespace SolrExpress.Solr5.Parameter
{
    public sealed class QueryFieldParameter : IParameter<JObject>
    {
        private readonly JProperty _value;

        /// <summary>
        /// Create a query field parameter
        /// </summary>
        /// <param name="query">Query used to make the query field</param>
        public QueryFieldParameter(string query)
        {
            this._value = new JProperty("qf", query);
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get { return true; } }

        /// <summary>
        /// Execute the creation of the parameter "query field"
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        public void Execute(JObject jObject)
        {
            var jObj = (JObject)jObject["params"] ?? new JObject();

            jObj.Add(this._value);

            jObject["params"] = jObj;
        }
    }
}