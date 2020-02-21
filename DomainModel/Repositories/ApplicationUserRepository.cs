using MyWalletApp.DomainModel.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyWalletApp.DomainModel.Repositories
{
    public class ApplicationUserRepository: IApplicationUserRepository
    {
        private ApplicationDbContext _dbContext  {get;}
        public ApplicationUserRepository(
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<ApplicationUser> GetByEmail(string email)
        {
            var normalizedEmail = email.ToUpper();

            return await _dbContext
                .ApplicationUsers
                .SingleOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail)
                .ConfigureAwait(false);
        }
        
    }
}