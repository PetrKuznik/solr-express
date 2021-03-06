﻿using Newtonsoft.Json.Linq;
using SolrExpress.Core.Builder;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Query;
using System;

namespace SolrExpress.Solr4.Builder
{
    /// <summary>
    /// Statistic data builder
    /// </summary>
    public sealed class StatisticResultBuilder<TDocument> : IStatisticResultBuilder<TDocument>, IConvertJsonObject
        where TDocument : IDocument
    {
        /// <summary>
        /// Execute the parse of the JSON object in statistic
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            if (jsonObject["response"]?["numFound"] != null && jsonObject["responseHeader"]?["QTime"] != null)
            {
                var documentCount = jsonObject["response"]["numFound"].ToObject<long>();
                var qTime = jsonObject["responseHeader"]["QTime"].ToObject<int>();

                this.Data = new Statistic
                {
                    DocumentCount = documentCount,
                    IsEmpty = documentCount.Equals(0),
                    ElapsedTime = new TimeSpan(0, 0, 0, 0, qTime)
                };

                return;
            }

            throw new UnexpectedJsonFormatException(jsonObject.ToString());
        }

        public Statistic Data { get; set; }
    }
}
