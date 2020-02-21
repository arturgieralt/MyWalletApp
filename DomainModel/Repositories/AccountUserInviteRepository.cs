using MyWalletApp.DomainModel.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.Services.Providers;
using MyWalletApp.Extensions;

namespace MyWalletApp.DomainModel.Repositories
{
    public class AccountUserInviteRepository: BaseRepository<AccountUserInvite>, IAccountUserInviteRepository
    {
        public AccountUserInviteRepository(
            ApplicationDbContext dbContext, 
            IDateTimeProvider dateTimeProvider,
            IUserContextProvider userContextProvider): base(dbContext, dateTimeProvider, userContextProvider)
        {

        }
        
        public async override Task<AccountUserInvite> GetById(long inviteId)
        {
            var userId = _userContextProvider.GetUser.GetId<string>();
            
            return await _dbContext
                .AccountUserInvites
                .SingleOrDefaultAsync(i => i.Id == inviteId && i.CreatedBy.Id == userId)
                .ConfigureAwait(false);
        }


        public async Task<bool> IsUserAlreadyInvited(string invitedUserId, long accountId)
        {
            return await _dbContext
                .AccountUserInvites
                .AnyAsync(i => i.UserId == invitedUserId && i.AccountId == accountId)
                .ConfigureAwait(false);
        }
        
    }
}