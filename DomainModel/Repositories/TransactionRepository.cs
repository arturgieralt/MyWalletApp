using MyWalletApp.DomainModel.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.Services.Providers;
using MyWalletApp.Extensions;

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

        public async override Task<Transaction> GetById(long transactionId)
        {
            var userId = _userContextProvider.GetUser.GetId<string>();

            return await _dbContext
                .Transactions
                .SingleOrDefaultAsync(t => t.Id == transactionId && t.CreatedBy.Id == userId)
                .ConfigureAwait(false);
        }
        
    }
}