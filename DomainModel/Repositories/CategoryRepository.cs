using MyWalletApp.DomainModel.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.Services.Providers;
using MyWalletApp.Extensions;

namespace MyWalletApp.DomainModel.Repositories
{
    public class CategoryRepository: BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(
            ApplicationDbContext dbContext, 
            IDateTimeProvider dateTimeProvider,
            IUserContextProvider userContextProvider): base(dbContext, dateTimeProvider, userContextProvider)
        {

        }

        public async override Task<Category> GetById(long categoryId)
        {
            var userId = _userContextProvider.GetUser.GetId<string>();
            
            return await _dbContext
                .Categories
                .SingleOrDefaultAsync(c => c.Id == categoryId && c.CreatedBy.Id == userId)
                .ConfigureAwait(false);
        }
        
    }
}