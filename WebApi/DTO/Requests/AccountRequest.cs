using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyWalletApp.WebApi.DTO.Requests
{
    public class AccountRequest
    {
        [Required]
        [JsonProperty("name")]
        public string Name {get; set;}

        [Required]
        [JsonProperty("currencyId")]
        public long CurrencyId { get; set; }
    }
}