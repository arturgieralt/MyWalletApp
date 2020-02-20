using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.DomainModel;
using MyWalletApp.Extensions;
using MyWalletApp.Services.Providers;
using MyWalletApp.WebApi.Queries.Common;
using TransactionApiModel = MyWalletApp.WebApi.Models.Transaction;

namespace MyWalletApp.WebApi.Queries.GetAllTransactions
{

    public class GetAllTransactionsQueryHandler: IRequestHandler<GetAllTransactionsQuery, GetAllTransactionsQueryResult>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUserContextProvider _userContextProvider;

        public GetAllTransactionsQueryHandler(ApplicationDbContext applicationDbContext, IUserContextProvider userContextProvider) 
        {
            _applicationDbContext = applicationDbContext;
            _userContextProvider = userContextProvider;
        }

        public async Task<GetAllTransactionsQueryResult> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextProvider.GetUser.GetId<string>();
            
            var transactionsQuery =  _applicationDbContext
                .Transactions
                .Include(t => t.TransactionTags)
                    .ThenInclude(tt => tt.Tag)
                .Where(t => t.Account.AccountUsers.Any(au => au.UserId == userId && !au.IsAccessRevoked));

            if(request.AccountId != null) 
            {
                transactionsQuery = transactionsQuery.Where(t => t.AccountId == request.AccountId);
            }

            
             var transactions = await transactionsQuery
                .Select(t => new TransactionApiModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    AccountId = t.AccountId,
                    Total = t.Total,
                    Date = t.Date,
                    TransactionType = t.TransactionType,
                    Category = t.Category,
                    Currency = t.Currency,
                    Tags = t.TransactionTags.Select(tt => tt.Tag.Name).ToList(),
                    Latitude = t.Latitude,
                    Longitude = t.Longitude
                })
                .OrderByDescending(t => t.Date)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new GetAllTransactionsQueryResult(){
                Status = QueryResultStatus.Success,
                Transactions = transactions
            };
        }
    }
}