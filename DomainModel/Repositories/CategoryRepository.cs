using MyWalletApp.DomainModel.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyWalletApp.DomainModel.Repositories
{
    public class CategoryRepository: BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext): base(dbContext)
        {

        }

        public async override Task<Category> GetById(long categoryId, string userId)
        {
            return await _dbContext
                .Categories
                .SingleOrDefaultAsync(c => c.Id == categoryId && c.CreatedBy.Id == userId)
                .ConfigureAwait(false);
        }
        
    }
}