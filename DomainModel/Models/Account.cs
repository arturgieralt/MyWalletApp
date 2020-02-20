using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWalletApp.DomainModel.Models
{
    [Table("Account")]
    public class Account: BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public long CurrencyId { get; set; }
        public Currency Currency {get; set;}

        public ICollection<Transaction> Transactions {get; set;}

        public ICollection<AccountUser> AccountUsers { get; set; }
        public Account(string name, long currencyId) {
            Name = name;
            CurrencyId = currencyId;
        }
        
    }
}