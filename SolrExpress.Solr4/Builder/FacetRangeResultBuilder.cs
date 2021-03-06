﻿using Newtonsoft.Json.Linq;
using SolrExpress.Core.Builder;
using SolrExpress.Core.Entity;
using SolrExpress.Core.Exception;
using SolrExpress.Core.Helper;
using SolrExpress.Core.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace SolrExpress.Solr4.Builder
{
    /// <summary>
    /// Facet range data builder
    /// </summary>
    public sealed class FacetRangeResultBuilder<TDocument> : IFacetRangeResultBuilder<TDocument>, IConvertJsonObject
        where TDocument : IDocument
    {
        /// <summary>
        /// Get a FacetRange instance basead in the informed JTokenType
        /// </summary>
        /// <param name="type">JTokenType used to return the instance</param>
        /// <returns>A FacetRange instance</returns>
        private FacetRange CreateFacetRange(JTokenType type)
        {
            switch (type)
            {
                case JTokenType.Float:
                    return new FacetRange<float>();
                case JTokenType.Date:
                    return new FacetRange<DateTime>();
                default:
                    return new FacetRange<int>();
            }
        }

        /// <summary>
        /// Returns the number part of the informed string
        /// </summary>
        /// <param name="value">String to clean</param>
        /// <returns>Number part of the value</returns>
        private int GetNumber(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? 0 : int.Parse(Regex.Replace(value, "[^0-9]", string.Empty, RegexOptions.IgnoreCase));
        }

        /// <summary>
        /// Calculate the gap in ranges of the informed facet
        /// </summary>
        /// <typeparam name="TFacetKey"></typeparam>
        /// <param name="facetData">Dictionary with facet data</param>
        /// <param name="gapValue">Gap of the facet range</param>
        /// <param name="facetBefore"></param>
        /// <param name="facetAfter"></param>
        private void CalculateGap<TFacetKey>(Dictionary<FacetRange, long> facetData, object gapValue, FacetRange facetBefore, FacetRange facetAfter)
            where TFacetKey : struct, IComparable
        {
            var first = facetData.First();
            var last = facetData.Last();

            if (typeof(TFacetKey) == typeof(DateTime))
            {
                foreach (var range in facetData)
                {
                    ((FacetRange<DateTime>)range.Key).MaximumValue = ((FacetRange<DateTime>)range.Key).MinimumValue.Value.Add((TimeSpan)gapValue);
                }
            }
            else
            {
                foreach (var range in facetData)
                {
                    ((FacetRange<TFacetKey>)range.Key).MaximumValue = GenericHelper.Addition(((FacetRange<TFacetKey>)range.Key).MinimumValue, (TFacetKey?)gapValue);
                }
            }

            ((FacetRange<TFacetKey>)facetBefore).MaximumValue = ((FacetRange<TFacetKey>)first.Key).MinimumValue;
            ((FacetRange<TFacetKey>)facetAfter).MinimumValue = ((FacetRange<TFacetKey>)last.Key).MaximumValue;
        }

        /// <summary>
        /// Process gap value in gap object
        /// </summary>
        /// <param name="gap">Gap string returned from Solr</param>
        /// <returns>.Net object equivalent</returns>
        private object GetGapValue(string gap)
        {
            int objInt;

            if (int.TryParse(gap, out objInt))
            {
                return objInt;
            }

            try
            {
                return Convert.ToSingle(gap, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
            }

            // Assuming than gap is DateTime type
            var gapNumber = this.GetNumber(gap);

            var keys = new Dictionary<string, DateTime>()
            {
                ["MILISECOND"] = DateTime.MinValue.AddMilliseconds(1),
                ["SECOND"] = DateTime.MinValue.AddSeconds(1),
                ["MINUTE"] = DateTime.MinValue.AddMinutes(1),
                ["HOUR"] = DateTime.MinValue.AddHours(1),
                ["DAY"] = DateTime.MinValue.AddDays(1),
                ["WEAK"] = DateTime.MinValue.AddDays(7),
                ["MONTH"] = DateTime.MinValue.AddMonths(1),
                ["YEAR"] = DateTime.MinValue.AddYears(1),
            };

            var key = keys.FirstOrDefault(q => gap.Contains(q.Key));

            return new TimeSpan(key.Value.Ticks * gapNumber);
        }

        /// <summary>
        /// Execute the parse of the JSON object in facet range list
        /// </summary>
        /// <param name="jsonObject">JSON object used in the parse</param>
        public void Execute(JObject jsonObject)
        {
            if (jsonObject["facet_counts"]?["facet_ranges"] == null)
            {
                throw new UnexpectedJsonFormatException(jsonObject.ToString());
            }

            var list = jsonObject["facet_counts"]["facet_ranges"]
                .Children()
                .ToList();

            this.Data = new List<FacetKeyValue<FacetRange>>();

            foreach (var item in list)
            {
                var facet = new FacetKeyValue<FacetRange>()
                {
                    Name = ((JProperty)item).Name,
                    Data = new Dictionary<FacetRange, long>()
                };

                var facetData = new Dictionary<FacetRange, long>();

                var gap = ((JValue)((JProperty)item).Value["gap"]).ToObject<string>();
                var array = (JArray)((JProperty)item).Value["counts"];

                var gapValue = this.GetGapValue(gap);

                var jTokenType = array[0].Type;

                if (jTokenType == JTokenType.String)
                {
                    int dummyInt;
                    float dummyFloat;

                    if (int.TryParse(array[0].ToString(), out dummyInt))
                    {
                        jTokenType = JTokenType.Integer;
                    }
                    else if (float.TryParse(array[0].ToString(), out dummyFloat))
                    {
                        jTokenType = JTokenType.Float;
                    }
                }

                for (var i = 0; i < array.Count; i += 2)
                {
                    var result = this.CreateFacetRange(jTokenType);

                    result.SetMinimumValue(array[i].ToObject(result.GetKeyType()));

                    facetData.Add(result, array[i + 1].ToObject<long>());
                }

                var before = this.CreateFacetRange(jTokenType);
                var after = this.CreateFacetRange(jTokenType);

                switch (jTokenType)
                {
                    case JTokenType.Float:
                        this.CalculateGap<float>(facetData, gapValue, before, after);
                        break;
                    case JTokenType.Date:
                        this.CalculateGap<DateTime>(facetData, gapValue, before, after);
                        break;
                    default:
                        this.CalculateGap<int>(facetData, gapValue, before, after);
                        break;
                }

                var beforeValue = ((JProperty)(item)).Value["before"].ToObject<long>();

                if (beforeValue > 0)
                {
                    facet.Data.Add(before, beforeValue);
                }

                foreach (var facetDataItem in facetData)
                {
                    facet.Data.Add(facetDataItem.Key, facetDataItem.Value);
                }

                var afterValue = ((JProperty)(item)).Value["after"].ToObject<long>();

                if (afterValue > 0)
                {
                    facet.Data.Add(after, afterValue);
                }

                this.Data.Add(facet);
            }
        }

        public List<FacetKeyValue<FacetRange>> Data { get; set; }
    }
}
