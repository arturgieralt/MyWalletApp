using System;
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

        public Category Currency {get; set;}   
    }
}