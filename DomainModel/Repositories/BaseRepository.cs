using System.Threading.Tasks;
using MyWalletApp.DomainModel.Models;
using MyWalletApp.Extensions;
using MyWalletApp.Services.Providers;

namespace MyWalletApp.DomainModel.Repositories
{
    public abstract class BaseRepository<T> where T: BaseModel
    {
        protected ApplicationDbContext _dbContext  {get; }
        private IDateTimeProvider _dateTimeProvider {get; }

        private IUserContextProvider _userContextProvider {get;}

        protected BaseRepository(
            ApplicationDbContext dbContext,
            IDateTimeProvider dateTimeProvider,
            IUserContextProvider userContextProvider
        ) 
        {
            _dbContext = dbContext;
            _dateTimeProvider = dateTimeProvider;
            _userContextProvider = userContextProvider;
        }

        public void SetAuditData(T entity, string userId) {
             if(entity.Id == default(long)) {
                entity.CreatedBy.Id = userId;
                entity.CreatedOn = _dateTimeProvider.GetCurrentUtcTime;
            }

            entity.LastModifiedBy.Id = userId;
            entity.LastModifiedOn = _dateTimeProvider.GetCurrentUtcTime;
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
            var user = _userContextProvider.GetUser;
            SetAuditData(entity, user.GetId<string>());
            
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