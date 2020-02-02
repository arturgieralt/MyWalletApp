using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWalletApp.DomainModel.Models
{
    [Table("Currency")]    
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {get; set;}
        public string Name {get; set;}
        public string ShortName {get; set;}
    }
}