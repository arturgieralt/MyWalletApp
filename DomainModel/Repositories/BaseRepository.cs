using System.Threading.Tasks;
using MyWalletApp.DomainModel.Models;

namespace MyWalletApp.DomainModel.Repositories
{
    public abstract class BaseRepository<T> where T: BaseModel
    {
        protected ApplicationDbContext _dbContext  {get; }

        protected BaseRepository(
            ApplicationDbContext dbContext
        ) 
        {
            _dbContext = dbContext;
        }

        public virtual async Task Delete(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public virtual async Task<long> Save(T entity)
        {
            if(entity.Id == default(long)) {
                var entityEntry = await _dbContext
                    .AddAsync(entity)
                    .ConfigureAwait(false);
                 return entityEntry.Entity.Id;
            }

            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return entity.Id;
        }

        public abstract Task<T> GetById(long entityId, string userId);
    }
}