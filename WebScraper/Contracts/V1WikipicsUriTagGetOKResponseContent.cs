// Template: Models (ApiModel.t4) version 3.0

// This code was generated by RAML Server Scaffolder

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace WebScraper.ScraperApi.Models
{
    public partial class V1WikipicsUriTagGetOKResponseContent
    {
        

        /// <summary>
        /// An explanation about the purpose of this instance.
        /// </summary>

		[JsonProperty("title")]
        public string Title { get; set; }


		[JsonProperty("images")]
        public IList<string> Images { get; set; }

        /// <summary>
        /// An explanation about the purpose of this instance.
        /// </summary>

		[JsonProperty("datetimeUTC")]
        public string DatetimeUTC { get; set; }
    } // end class

} // end Models namespace

