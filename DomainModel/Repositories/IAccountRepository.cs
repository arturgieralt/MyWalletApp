using System.Threading.Tasks;
using MyWalletApp.DomainModel.Models;

namespace MyWalletApp.DomainModel.Repositories
{
    public interface IAccountRepository
    {
         Task<Account> GetById(long accountId);
         Task<bool> DoesExistForUser(long entityId);
         Task<long> Save(Account account);
         Task Delete (Account account);

    }
}