using System.Collections.Generic;
using System.Threading.Tasks;
using SuitcaseApi.DTO.Requests;
using SuitcaseApi.DTO.Responses;

namespace SuitcaseApi.Repositories.Interfaces
{
    public interface ISuitcaseDbRepository
    {
        Task<ICollection<SuitcaseDetailsResponseDto>> GetSuitcasesOnSpecificTripFromDb(int cityId);
        Task<ICollection<SuitcaseResponseDto>> FindSuitcaseFromDb(string text);
        Task<bool> AddItemToSuitcase(ItemRequestDto itemRequestDto, int suitcaseId);
        Task<bool> DeleteSuitcasesItemFromDb(SuitcaseItemRequestDto suitcaseItemRequestDto);
        Task<bool> DeleteSuitcaseFromDb(int suitcaseId);
    }
}