using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyWalletApp.WebApi.DTO.Requests
{
    public class CategoryRequest
    {
        [Required]
        [JsonProperty("name")]
        public string Name {get; set;}

    }
}