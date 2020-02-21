using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyWalletApp.WebApi.DTO.Requests
{
    public class UserInviteRequest
    {

        [Required]
        [JsonProperty("email")]
        public string Email {get; set;}

        [Required]
        [JsonProperty("transactionRead")]
        public bool TransactionRead { get; set; }

        [Required]
        [JsonProperty("transactionWrite")]
        public bool TransactionWrite { get; set; }

        [Required]
        [JsonProperty("accountDelete")]
        public bool AccountDelete { get; set; }

        [Required]
        [JsonProperty("accountWrite")]
        public bool AccountWrite { get; set; }
    }
}