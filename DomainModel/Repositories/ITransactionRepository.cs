using System.Threading.Tasks;
using MyWalletApp.DomainModel.Models;

namespace MyWalletApp.DomainModel.Repositories
{
    public interface ITransactionRepository
    {
         Task<Transaction> GetById(long transactionId, string userId);
    }
}