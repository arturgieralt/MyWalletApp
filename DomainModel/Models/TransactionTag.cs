using System.ComponentModel.DataAnnotations;

namespace MyWalletApp.DomainModel.Models
{
    public class TransactionTag
    {
        public long TransactionId {get; set;}
        public Transaction Transaction { get; set; }

        public long TagId { get; set; }
        public Tag Tag {get; set; }
    }
}