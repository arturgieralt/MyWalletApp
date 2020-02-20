using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWalletApp.DomainModel.Models
{
    [Table("AccountUserInvite")]
    public class AccountUserInvite: BaseModel
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
    }
}