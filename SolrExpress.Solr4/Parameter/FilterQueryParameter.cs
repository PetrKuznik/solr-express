﻿using SolrExpress.Core.Helper;
using SolrExpress.Core.Parameter;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;

namespace SolrExpress.Solr4.Parameter
{
    public sealed class FilterQueryParameter : IFilterParameter, IParameter<List<string>>
    {
        private readonly IQueryParameterValue _value;
        private readonly string _tagName;

        /// <summary>
        /// Create a filter parameter
        /// </summary>
        /// <param name="value">Parameter value used to create the query</param>
        /// <param name="tagName">Tag name to use in facet excluding list</param>
        public FilterQueryParameter(IQueryParameterValue value, string tagName = null)
        {
            ThrowHelper<ArgumentNullException>.If(value == null);

            this._value = value;
            this._tagName = tagName;
        }

        /// <summary>
        /// True to indicate multiple instances of the parameter, otherwise false
        /// </summary>
        public bool AllowMultipleInstances { get; } = true;

        /// <summary>
        /// Execute the creation of the parameter "fq"
        /// </summary>
        /// <param name="container">Container to parameters to request to SOLR</param>
        public void Execute(List<string> container)
        {
            container.Add($"fq={UtilHelper.GetSolrFilterWithTag(this._tagName, this._value.Execute())}");
        }
    }
}
