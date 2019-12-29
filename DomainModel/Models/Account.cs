using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWalletApp.DomainModel.Models
{
    [Table("Account")]
    public class Account: BaseModel
    {
        private readonly List<Transaction> _transactions = new List<Transaction>();
        [Required]
        public string Name { get; set; }

        [Required]
        public long CurrencyId { get; set; }
        public Currency Currency {get; set;}

        public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();

        public Account(string name, long currencyId) {
            Name = name;
            CurrencyId = currencyId;
        }
        
    }
}