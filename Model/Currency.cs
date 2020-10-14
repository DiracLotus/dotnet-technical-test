using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace dotnet_technical_test.Model
{
    /// <summary>
    /// Represents the response from the given currency API.
    /// </summary>
    public class Currency
    {
        [JsonProperty("base")]
        public string Name { get; set; }

        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonProperty("time_last_updated")]
        public string TimeLastUpdated { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
