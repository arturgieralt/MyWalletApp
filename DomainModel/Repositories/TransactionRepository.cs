using MyWalletApp.DomainModel.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.Services.Providers;

namespace MyWalletApp.DomainModel.Repositories
{
    public class TransactionRepository: BaseRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(
            ApplicationDbContext dbContext, 
            IDateTimeProvider dateTimeProvider,
            IUserContextProvider userContextProvider): base(dbContext, dateTimeProvider, userContextProvider)
        {

        }

        public async override Task<Transaction> GetById(long transactionId, string userId)
        {
            return await _dbContext
                .Transactions
                .SingleOrDefaultAsync(t => t.Id == transactionId && t.CreatedBy.Id == userId)
                .ConfigureAwait(false);
        }
        
    }
}