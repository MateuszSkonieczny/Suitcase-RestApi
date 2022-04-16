using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SuitcaseApi.DTO.Requests;
using SuitcaseApi.DTO.Responses;
using SuitcaseApi.Models;
using SuitcaseApi.Repositories.Interfaces;

namespace SuitcaseApi.Repositories.Implementations
{
    public class SuitcaseMssqlDbRepository: ISuitcaseDbRepository
    {
        private readonly SuitcaseContext _context;

        public SuitcaseMssqlDbRepository(SuitcaseContext context)
        {
            _context = context;
        }
        
        public async Task<ICollection<SuitcaseDetailsResponseDto>> GetSuitcasesOnSpecificTripFromDb(int cityId)
        {
            var res = await _context.Suitcase.Where(e => e.IdCity == cityId)
                .Include(e => e.IdCityNavigation)
                .ThenInclude(e => e.IdCountryNavigation)
                .Select(e => new SuitcaseDetailsResponseDto
                {
                    Name = e.Name,
                    City = e.IdCityNavigation.Name,
                    Country = e.IdCityNavigation.IdCountryNavigation.Name,
                    Items = _context.SuitcaseItem.Where(t => t.IdSuitcase == e.IdSuitcase)
                        .Include(t => t.IdItemNavigation)
                        .Select(t => new ItemResponseDto
                        {
                            Name = t.IdItemNavigation.Name,
                            IsPacked = t.IdItemNavigation.IsPacked,
                            Quantity = t.IdItemNavigation.Quantity
                        }).ToList()
                }).ToListAsync();

            return res.Count == 0 ? null : res;
        }

        public async Task<ICollection<SuitcaseResponseDto>> FindSuitcaseFromDb(string text)
        {
            var res = await _context.Suitcase.Where(e => e.Name.Contains(text))
                .Select(e => new SuitcaseResponseDto
                {
                    Name = e.Name
                })
                .ToListAsync();
            return res.Count == 0 ? null : res;
        }

        public async Task<bool> AddItemToSuitcase(ItemRequestDto itemRequestDto, int suitcaseId)
        {
            var suitcase = await _context.Suitcase.Where(e => e.IdSuitcase == suitcaseId).SingleOrDefaultAsync();
            if (suitcase is null)
                return false;

            var existingItem = await _context.Item.Where(e => e.Name == itemRequestDto.Name
                                                              && e.Quantity == itemRequestDto.Quantity
                                                              && e.IsPacked == itemRequestDto.IsPacked)
                .SingleOrDefaultAsync();
            if (existingItem is not null)
            {
                await _context.AddAsync(new SuitcaseItem
                {
                    IdItem = existingItem.IdItem,
                    IdSuitcase = suitcaseId
                });
                await _context.SaveChangesAsync();
                return true;
            }

            await _context.AddAsync(new Item
            {
                Name = itemRequestDto.Name,
                Quantity = itemRequestDto.Quantity,
                IsPacked = itemRequestDto.IsPacked
            });

            var newItemId = _context.Item.Max(e => e.IdItem);

            await _context.AddAsync(new SuitcaseItem
            {
                IdItem = newItemId,
                IdSuitcase = suitcaseId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSuitcasesItemFromDb(SuitcaseItemRequestDto suitcaseItemRequestDto)
        {
            var res = await _context.SuitcaseItem
                            .SingleOrDefaultAsync(e => e.IdItem == suitcaseItemRequestDto.ItemId 
                                                       && e.IdSuitcase == suitcaseItemRequestDto.SuitcaseId);
            if (res is null)
                return false;
            
            _context.Remove(res);
            return true;
        }

        public async Task<bool> DeleteSuitcaseFromDb(int suitcaseId)
        {
            var suitcaseItems = await _context.SuitcaseItem
                .Where(e => e.IdSuitcase == suitcaseId).ToListAsync();
            var suitcase = await _context.Suitcase
                .Where(e => e.IdSuitcase == suitcaseId).SingleOrDefaultAsync();

            if (suitcase is null)
                return false;
            
            if (suitcaseItems.Count != 0)
                _context.RemoveRange(suitcaseItems);
            
            _context.Remove(suitcase);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}