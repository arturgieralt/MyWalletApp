using System.Threading.Tasks;
using MyWalletApp.DomainModel.Models;

namespace MyWalletApp.DomainModel.Repositories
{
    public interface IAccountUserInviteRepository
    {
         Task<bool> IsUserAlreadyInvited(string invitedUserId, long accountId);
         Task<AccountUserInvite> GetById(long inviteId);
         Task<long> Save(AccountUserInvite invite);
         Task Delete(AccountUserInvite invite);
    }
}