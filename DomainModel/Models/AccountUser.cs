using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWalletApp.DomainModel.Models
{
    [Table("AccountUser")]
    public class AccountUser
    {
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        public long AccountId { get; set; }
        public Account Account { get; set; }

        [Required]
        public bool TransactionRead { get; set; }

        [Required]
        public bool TransactionWrite { get; set; }

        [Required]
        public bool AccountDelete { get; set; }

        [Required]
        public bool AccountWrite { get; set; }

        [Required]
        public bool IsAccessRevoked { get; set; }

        public AccountUser(string userId,
         long accountId,
         bool transactionRead = true,
         bool transactionWrite = true,
         bool accountDelete = true,
         bool accountWrite = true,
         bool isAccessRevoked = false ) 
         {
             AccountId = accountId;
             UserId = userId;
             TransactionWrite = transactionWrite;
             TransactionRead = transactionRead;
             AccountDelete = accountDelete;
             AccountWrite = accountWrite;
             IsAccessRevoked = isAccessRevoked;
         }
    }
}