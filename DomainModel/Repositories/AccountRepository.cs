using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.Services.Providers;

namespace MyWalletApp.DomainModel.Repositories
{
    public class AccountRepository: BaseRepository<Account> , IAccountRepository
    {
        public AccountRepository(
            ApplicationDbContext dbContext, 
            IDateTimeProvider dateTimeProvider,
            IUserContextProvider userContextProvider): base(dbContext, dateTimeProvider, userContextProvider)
        {

        }

        public async override Task<Account> GetById(long accountId, string userId)
        {
            return await _dbContext
                .Accounts
                .Include(a => a.Transactions)
                .SingleOrDefaultAsync(a => a.Id == accountId && a.CreatedBy.Id == userId)
                .ConfigureAwait(false);

        }
        
    }
}