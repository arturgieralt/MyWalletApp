using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWalletApp.DomainModel.Models
{
    [Table("AccountPermission")]
    public class AccountPermission: BaseModel
    {
        [Required]
        public bool Read { get; set; }

        [Required]
        public bool Write { get; set; }
    }
}