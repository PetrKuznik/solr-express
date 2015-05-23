﻿using Newtonsoft.Json.Linq;

namespace SolrExpress.QueryBuilder
{
    /// <summary>
    /// Signatures to use in solr query parameter
    /// </summary>
    public interface IQueryParameter
    {
        /// <summary>
        /// True to indicate multiple instance of the parameter, otherwise false
        /// </summary>
        bool AllowMultipleInstance { get; }

        /// <summary>
        /// Parameter name
        /// </summary>
        string ParameterName { get; }

        /// <summary>
        /// Execute the creation of the parameter
        /// </summary>
        /// <param name="jObject">JSON object with parameters to request to SOLR</param>
        void Execute(JObject jObject);
    }
}
