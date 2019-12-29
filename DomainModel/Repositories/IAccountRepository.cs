using System.Threading.Tasks;
using MyWalletApp.DomainModel.Models;

namespace MyWalletApp.DomainModel.Repositories
{
    public interface IAccountRepository
    {
         Task<Account> GetById(long accountId, string userId);
         Task<long> Save(Account account);
    }
}