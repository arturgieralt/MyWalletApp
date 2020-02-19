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
        public long AccountPermissionId { get; set; }
        public AccountPermission AccountPermission { get; set; }
    }
}