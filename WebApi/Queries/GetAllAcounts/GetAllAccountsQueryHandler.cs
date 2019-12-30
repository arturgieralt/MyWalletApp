using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.DomainModel;
using MyWalletApp.Extensions;
using MyWalletApp.Services.Providers;
using MyWalletApp.WebApi.Models;
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
                .Include(a => a.Currency)
                .Where(a => a.CreatedById == userId)
                .Select( a => new AccountSummary{
                    Id = a.Id,
                    Name = a.Name,
                    Balance = a.Transactions.Sum(t => t.Total),
                    TransactionCount = a.Transactions.Count(),
                    Currency = a.Currency,
                    CreatedOn = a.CreatedOn
                })
                .OrderBy(a => a.Name)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new GetAllAccountsQueryResult(){
                Status = QueryResultStatus.Success,
                Accounts = accounts
            };
        }
    }
}