using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.Extensions;
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

        public async override Task<Account> GetById(long accountId)
        {
            var userId = _userContextProvider.GetUser.GetId<string>();
            
            return await _dbContext
                .Accounts
                .Include(a => a.Transactions)
                .SingleOrDefaultAsync(a => a.Id == accountId && a.CreatedBy.Id == userId)
                .ConfigureAwait(false);

        }

        public virtual async Task<bool> DoesExistForUser(long entityId)
        {
            var userId = _userContextProvider.GetUser.GetId<string>();

            return await _dbContext
                .Accounts
                .AnyAsync(a => a.Id == entityId && a.CreatedById == userId)
                .ConfigureAwait(false);
        }

        
    }
}