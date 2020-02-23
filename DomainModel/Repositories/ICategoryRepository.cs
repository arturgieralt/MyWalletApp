using System.Threading.Tasks;
using MyWalletApp.DomainModel.Models;

namespace MyWalletApp.DomainModel.Repositories
{
    public interface ICategoryRepository
    {
         Task<Category> GetById(long categoryId);
         Task<bool> DoesNameExist(string name);
         Task<long> Save(Category category);
         Task Delete(Category category);
    }
}