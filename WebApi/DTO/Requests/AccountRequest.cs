using System.ComponentModel.DataAnnotations;
using MyWalletApp.WebApi.Models;
using Newtonsoft.Json;

namespace MyWalletApp.WebApi.DTO.Requests
{
    public class AccountRequest
    {
        [Required]
        [JsonProperty("name")]
        public string Name {get; set;}

        [Required]
        [JsonProperty("currency")]
        public Currency Currency { get; set; }
    }
}