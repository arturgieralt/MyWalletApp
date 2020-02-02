using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.DomainModel;
using MyWalletApp.Extensions;
using MyWalletApp.Services.Providers;
using MyWalletApp.WebApi.Queries.Common;

namespace MyWalletApp.WebApi.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler: IRequestHandler<GetAllCategoriesQuery, GetAllCategoriesQueryResult>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUserContextProvider _userContextProvider;

        public GetAllCategoriesQueryHandler(ApplicationDbContext applicationDbContext, IUserContextProvider userContextProvider) 
        {
            _applicationDbContext = applicationDbContext;
            _userContextProvider = userContextProvider;
        }

        public async Task<GetAllCategoriesQueryResult> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContextProvider.GetUser.GetId<string>();
            
            var categories = await _applicationDbContext
                .Categories
                .Where(c => c.CreatedById == userId)
                .OrderBy(c => c.Name)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new GetAllCategoriesQueryResult(){
                Status = QueryResultStatus.Success,
                Categories = categories
            };
        }
    }
}