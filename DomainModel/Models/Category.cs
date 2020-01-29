using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWalletApp.DomainModel.Models
{
    [Table("Category")]    
    public class Category: BaseModel
    {
        private readonly List<Transaction> _transactions = new List<Transaction>();
        
        [Required]
        public string Name {get; set;}
        public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();
        
    }
}