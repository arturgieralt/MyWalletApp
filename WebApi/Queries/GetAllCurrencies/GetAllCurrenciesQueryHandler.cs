using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.DomainModel;
using MyWalletApp.WebApi.Queries.Common;

namespace MyWalletApp.WebApi.Queries.GetAllCurrencies
{
    public class GetAllCurrenciesQueryHandler: IRequestHandler<GetAllCurrenciesQuery, GetAllCurrenciesQueryResult>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GetAllCurrenciesQueryHandler(ApplicationDbContext applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<GetAllCurrenciesQueryResult> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
        {

            var currencies = await _applicationDbContext
                .Currencies
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new GetAllCurrenciesQueryResult(){
                Status = QueryResultStatus.Success,
                Currencies = currencies
            };
        }
    }
}