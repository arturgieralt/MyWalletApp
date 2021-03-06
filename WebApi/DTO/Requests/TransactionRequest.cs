using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyWalletApp.WebApi.Models;
using Newtonsoft.Json;

namespace MyWalletApp.WebApi.DTO.Requests
{
    public class TransactionRequest
    {
        [Required]
        [JsonProperty("name")]
        public string Name {get; set; }

        [Required]
        [JsonProperty("accountId")]
        public long AccountId { get; set; }

        [Required]
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [Required]
        [JsonProperty("total")]
        public decimal Total { get; set; }

        [Required]
        [JsonProperty("transactionType")]
        public TransactionType TransactionType { get; set; }
        
        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        [JsonProperty("categoryId")]
        public long? CategoryId { get; set; }

        [JsonProperty("latitude")]
        public decimal? Latitude { get; set; }

        [JsonProperty("longitude")]
        public decimal? Longitude { get; set; }

    }
}