using System.Threading.Tasks;
using MyWalletApp.DomainModel.Models;

namespace MyWalletApp.DomainModel.Repositories
{
    public interface ICategoryRepository
    {
         Task<Category> GetById(long categoryId, string userId);
    }
}