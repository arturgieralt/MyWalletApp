using System;
using System.Collections.Generic;
using MyWalletApp.DomainModel.Models;

namespace MyWalletApp.WebApi.Models
{
    public class Transaction
    {
        public long Id {get; set;}
        public string Name { get; set; }  

        public long AccountId {get; set; }

        public decimal Total { get; set; }

        public DateTime Date { get; set; }

        // think what to do with mapping
        public MyWalletApp.DomainModel.Models.TransactionType TransactionType { get; set; }

        public Category Category {get; set;} 

        public Currency Currency {get; set;}   

        public ICollection<string> Tags {get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }
    }
}