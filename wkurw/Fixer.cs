using System.Collections.Generic;

using Newtonsoft.Json;

namespace wkurw
{
    public class Fixer
    {
        [JsonProperty(PropertyName = "base")]
        public string BaseCurrency { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "rates")]
        public Dictionary<string, string> Rates { get; set; }
    }
}