using System;
using MyWalletApp.DomainModel.Models;

namespace MyWalletApp.WebApi.Models
{
    public class AccountSummary
    {
        public long Id {get; set;}
        public string Name {get; set;}
        public decimal Balance {get; set;}
        public long TransactionCount {get; set;}
        public Currency Currency {get; set;}

        public DateTime CreatedOn {get; set;}
    }
}