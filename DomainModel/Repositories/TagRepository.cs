using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.Extensions;
using MyWalletApp.Services.Providers;

namespace MyWalletApp.DomainModel.Repositories
{
    public class TagRepository: BaseRepository<Tag> , ITagRepository
    {
        public TagRepository(
            ApplicationDbContext dbContext, 
            IDateTimeProvider dateTimeProvider,
            IUserContextProvider userContextProvider): base(dbContext, dateTimeProvider, userContextProvider)
        {

        }

        public async override Task<Tag> GetById(long tagId)
        {
            var userId = _userContextProvider.GetUser.GetId<string>();
            
            return await _dbContext
                .Tags
                .SingleOrDefaultAsync(t => t.Id == tagId && t.CreatedBy.Id == userId)
                .ConfigureAwait(false);

        }

        public async Task<Tag> GetByName(string name)
        {
            var userId = _userContextProvider.GetUser.GetId<string>();
            
            return await _dbContext
                .Tags
                .SingleOrDefaultAsync(t => t.Name == name && t.CreatedBy.Id == userId)
                .ConfigureAwait(false);

        }
        
    }
}