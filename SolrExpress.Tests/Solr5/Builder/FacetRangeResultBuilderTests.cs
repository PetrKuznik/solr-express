﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using SolrExpress.Solr5.Builder;

namespace SolrExpress.Tests.Solr5.Builder
{
    [TestClass]
    public class FacetRangeResultBuilderTests
    {
        [TestMethod]
        public void WhenExecute_CreateAListWithFacetsParsedInConcretClasses()
        {
            // Arrange
            var jObject = JObject.Parse(@"
            {
                ""facets"": {
                    ""count"": 100,
                    ""facetRange"": {
                          ""buckets"": [
                            {
                              ""val"": 100,
                              ""count"": 10
                            },
                            {
                              ""val"": 200,
                              ""count"": 20
                            }
                          ],
                          ""before"": {
                            ""count"": 30
                          },
                          ""after"": {
                            ""count"": 40
                          }
                        }
                }
            }");

            var parameter = new FacetRangeResultBuilder();

            // Act
            parameter.Execute(jObject);

            // Assert
            Assert.AreEqual(1, parameter.Data.Count);
            Assert.AreEqual("facetRange", parameter.Data[0].Name);
            Assert.AreEqual(4, parameter.Data[0].Data.Count);
        }
    }
}
