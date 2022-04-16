using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuitcaseApi.DTO.Requests;
using SuitcaseApi.Repositories.Interfaces;

namespace SuitcaseApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/suticases")]
    public class SuitcasesController: ControllerBase
    {
        private readonly ISuitcaseDbRepository _suitcaseDbRepository;

        public SuitcasesController(ISuitcaseDbRepository suitcaseDbRepository)
        {
            _suitcaseDbRepository = suitcaseDbRepository;
        }

        [HttpGet("{cityId:int}")]
        public async Task<IActionResult> GetSuitcasesOnSpecificTrip([FromRoute]int cityId)
        {
            var res = await _suitcaseDbRepository.GetSuitcasesOnSpecificTripFromDb(cityId);
            if (res is null)
                return NotFound("Suitcase for given city id not found!");
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> FindSuitcaseWithText([FromBody] string text)
        {
            var res = await _suitcaseDbRepository.FindSuitcaseFromDb(text);
            if (res is null)
                return NotFound($"No suitcase contains: {text}");
            return Ok(res);
        }

        [HttpPost("{suitcaseId:int}")]
        public async Task<IActionResult> AddItemToSuitcase([FromBody] ItemRequestDto itemRequestDto, int suitcaseId)
        {
            var res = await _suitcaseDbRepository.AddItemToSuitcase(itemRequestDto, suitcaseId);
            if (!res)
                return NotFound("Suitcase was with given id not found");
            return Ok("Item added to suitcase");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItemFromSuitcase([FromBody] SuitcaseItemRequestDto suitcaseItemRequestDto)
        {
            var res = await _suitcaseDbRepository.DeleteSuitcasesItemFromDb(suitcaseItemRequestDto);
            if (!res)
                return NotFound("There is no item with given id in choosen suitcase");
            return Ok("Item deleted from choosen suitcase");
        }

        [HttpDelete("{suitcaseId:int}")]
        public async Task<IActionResult> DeleteSuitcase([FromRoute] int suitcaseId)
        {
            var res = await _suitcaseDbRepository.DeleteSuitcaseFromDb(suitcaseId);
            if (!res)
                return NotFound("Suitcase with given id not found");
            return Ok("Suitcase with given id successfully deleted");
        }

    }
}