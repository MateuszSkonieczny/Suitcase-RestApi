using System.Threading.Tasks;

namespace SuitcaseApi.Repositories.Interfaces
{
    public interface IItemDbRepository
    {
        Task<bool> MarkItemAsPacked(int itemId);
    }
}