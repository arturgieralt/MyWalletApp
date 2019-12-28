using System.ComponentModel.DataAnnotations.Schema;

namespace MyWalletApp.DomainModel.Models
{
    [Table("Category")]    
    public class Category: BaseModel
    {
        public string Name {get; set;}
    }
}