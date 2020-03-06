using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWalletApp.DomainModel.Models
{
        
    [Table("ExchangeRate")]    
    public class ExchangeRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id {get; set;}
        public DateTime Date {get; set; }

        public long BaseCurrencyId {get; set;}
        public Currency BaseCurrency { get; set; }

        public long CurrencyId {get; set; }
        public Currency Currency { get; set; }

        public decimal Rate { get; set; }
    }
}