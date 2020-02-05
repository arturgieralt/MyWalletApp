using System.Threading.Tasks;
using MyWalletApp.DomainModel.Models;

namespace MyWalletApp.DomainModel.Repositories
{
    public interface ITagRepository
    {
        Task<Tag> GetById(long transactionId);
        Task<Tag> GetByName(string name);
        Task<long> Save(Tag tag);
    }
}