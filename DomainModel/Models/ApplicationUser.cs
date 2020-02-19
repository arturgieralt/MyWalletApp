using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MyWalletApp.DomainModel.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<AccountUser> AccountUsers { get; set; }
    }
}
