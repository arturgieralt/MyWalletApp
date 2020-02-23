using System.Threading.Tasks;
using MyWalletApp.DomainModel.Models;

namespace MyWalletApp.DomainModel.Repositories
{
    public interface IApplicationUserRepository
    {
         Task<ApplicationUser> GetByEmail(string email);
    }
}