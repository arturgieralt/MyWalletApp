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
        public long AccountPermissionId { get; set; }
        public AccountPermission AccountPermission { get; set; }
        public bool IsAccessRevoked { get; set; }
    }
}