using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWalletApp.DomainModel.Models
{
    public class BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; protected set; }

        [Required]
        public ApplicationUser CreatedBy { get; protected set; }

        [Required]
        public DateTime CreatedOn { get; protected set; }

        [Required]
        public ApplicationUser LastModifiedBy { get; protected set; }

        [Required]
        public DateTime LastModifiedOn { get; protected set; }
    }
}