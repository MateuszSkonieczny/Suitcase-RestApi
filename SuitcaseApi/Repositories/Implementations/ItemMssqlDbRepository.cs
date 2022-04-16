using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuitcaseApi.Models;
using SuitcaseApi.Repositories.Interfaces;

namespace SuitcaseApi.Repositories.Implementations
{
    public class ItemMssqlDbRepository: IItemDbRepository
    {
        private readonly SuitcaseContext _context;

        public ItemMssqlDbRepository(SuitcaseContext context)
        {
            _context = context;
        }
        
        public async Task<bool> MarkItemAsPacked(int itemId)
        {
            var item = await _context.Item.SingleOrDefaultAsync(e => e.IdItem == itemId);
            if (item is null)
                return false;

            item.IsPacked = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}