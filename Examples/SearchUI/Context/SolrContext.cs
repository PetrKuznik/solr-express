﻿using SearchUI.Models;
using SolrExpress.Core.Query;
using SolrExpress.Solr5;
using System;

namespace SearchUI.Context
{
    public class SolrContext : IDisposable
    {
        public SolrContext()
        {
            var provider = new Provider("http://localhost:8983/solr/techproducts");

            this.TechProducts = new SolrQueryable<TechProduct>(provider);
        }

        public SolrQueryable<TechProduct> TechProducts { get; private set; }

        public void Dispose()
        {
        }
    }
}