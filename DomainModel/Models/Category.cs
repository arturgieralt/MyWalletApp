using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWalletApp.DomainModel.Models
{
    [Table("Category")]    
    public class Category: BaseModel
    {
        
        [Required]
        public string Name {get; set;}
        public ICollection<Transaction> Transactions {get; set;}
        
    }
}