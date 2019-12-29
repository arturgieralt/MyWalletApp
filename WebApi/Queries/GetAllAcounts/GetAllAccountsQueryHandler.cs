using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.DomainModel;
using MyWalletApp.Extensions;
using MyWalletApp.Services.Providers;
using MyWalletApp.WebApi.Queries.Common;

namespace MyWalletApp.WebApi.Queries.GetAllAcounts
{
    public class GetAllAccountsQueryHandler: IRequestHandler<GetAllAccountsQuery, GetAllAccountsQueryResult>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUserContextProvider _userContextProvider;

        public GetAllAccountsQueryHandler(ApplicationDbContext applicationDbContext, IUserContextProvider userContextProvider) 
        {
            _applicationDbContext = applicationDbContext;
            _userContextProvider = userContextProvider;
        }

        public async Task<GetAllAccountsQueryResult> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {

            var userId = _userContextProvider.GetUser.GetId<string>();
            
            var accounts = await _applicationDbContext
                .Accounts
                .Where(a => a.CreatedById == userId)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new GetAllAccountsQueryResult(){
                Status = QueryResultStatus.Success,
                Accounts = accounts
            };
        }
    }
}