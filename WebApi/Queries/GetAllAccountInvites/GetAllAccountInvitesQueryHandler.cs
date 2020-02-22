using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.DomainModel;
using MyWalletApp.Extensions;
using MyWalletApp.Services.Providers;
using MyWalletApp.WebApi.DTO.Results;
using MyWalletApp.WebApi.Queries.Common;

namespace MyWalletApp.WebApi.Queries.GetAllAccountInvites
{
     public class GetAllAccountInvitesQueryHandler: IRequestHandler<GetAllAccountInvitesQuery, GetAllAccountInvitesQueryResult>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUserContextProvider _userContextProvider;

        public GetAllAccountInvitesQueryHandler(ApplicationDbContext applicationDbContext, IUserContextProvider userContextProvider) 
        {
            _applicationDbContext = applicationDbContext;
            _userContextProvider = userContextProvider;
        }

        public async Task<GetAllAccountInvitesQueryResult> Handle(GetAllAccountInvitesQuery request, CancellationToken cancellationToken)
        {

            var userId = _userContextProvider.GetUser.GetId<string>();
            
            var invites = await _applicationDbContext
                .AccountUserInvites
                .Include(i => i.Account)
                .Include(i => i.User)
                .Include(i => i.CreatedBy) // all data returned!!
                .Where(i => i.CreatedById == userId || i.UserId == userId)
                .Select(i => new AccountInviteResult() 
                {
                    Id = i.Id,
                    Invited = new UserResult() 
                    {  
                        Id = i.User.Id,
                        Name = i.User.UserName
                    },
                    InvitedBy = new UserResult() 
                    {  
                        Id = i.CreatedBy.Id,
                        Name = i.CreatedBy.UserName
                    },
                    AccountId = i.Account.Id,
                    AccountName = i.Account.Name,
                    TransactionRead = i.TransactionRead,
                    TransactionWrite = i.TransactionWrite,
                    AccountDelete = i.AccountDelete,
                    AccountWrite = i.AccountWrite
                })
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new GetAllAccountInvitesQueryResult(){
                Status = QueryResultStatus.Success,
                AccountInvites = invites
            };
        }
    }
}