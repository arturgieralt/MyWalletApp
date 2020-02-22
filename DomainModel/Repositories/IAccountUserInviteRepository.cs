using System.Threading.Tasks;
using MyWalletApp.DomainModel.Models;

namespace MyWalletApp.DomainModel.Repositories
{
    public interface IAccountUserInviteRepository
    {
         Task<bool> IsUserAlreadyInvited(string invitedUserId, long accountId);
         Task<AccountUserInvite> GetById(long inviteId);
         Task<AccountUserInvite> GetByIdForUser(long inviteId, string userId);
         Task<long> Save(AccountUserInvite invite);
         Task Delete(AccountUserInvite invite);
    }
}