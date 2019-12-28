using MyWalletApp.DomainModel.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyWalletApp.DomainModel.Repositories
{
    public class TransactionRepository: BaseRepository<Transaction> 
    {
        public TransactionRepository(ApplicationDbContext dbContext): base(dbContext)
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