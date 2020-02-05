using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWalletApp.DomainModel.Models
{
    [Table("Transaction")]    
    public class Transaction: BaseModel
    {

        [Required]
        public string Name { get; set; }  

        [Required]
        public long AccountId {get; set; }
        public Account Account { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        public long? CategoryId { get; set; }
        public Category Category {get; set;} 

        [Required]
        public long CurrencyId { get; set; }
        public Category Currency {get; set; }   

        public ICollection<TransactionTag> TransactionTags {get;set;}

        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        
    }
}