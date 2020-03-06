using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyWalletApp.ExternalData
{
    public class ExchangeRate : Dictionary<string, decimal>
    {
    }

    public class ExchangeRatesApiResponse
    {
        [Required]
        [JsonProperty("rates")]
        public Dictionary<string, ExchangeRate> Rates { get; set; }

        [Required]
        [JsonProperty("end_at")]
        public string EndAt { get; set; }

        [Required]
        [JsonProperty("start_at")]
        public string StartAt { get; set; }

        [Required]
        [JsonProperty("base")]
        public string Base { get; set; }
    }
}