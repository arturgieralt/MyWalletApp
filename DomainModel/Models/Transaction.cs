using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWalletApp.DomainModel.Models
{
    [Table("Transaction")]    
    public class Transaction: BaseModel
    {
        public string Name { get; set; }  

        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        public decimal Total { get; set; }

        public DateTime Date { get; set; }

        public TransactionType TransactionType { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category {get; set;}   
    }
}