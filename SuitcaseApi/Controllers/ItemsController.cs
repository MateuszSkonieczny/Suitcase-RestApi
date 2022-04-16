using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuitcaseApi.Repositories.Interfaces;

namespace SuitcaseApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/items")]
    public class ItemsController: ControllerBase
    {
        private readonly IItemDbRepository _itemDbRepository;

        public ItemsController(IItemDbRepository itemDbRepository)
        {
            _itemDbRepository = itemDbRepository;
        }

        [HttpPut("{idItem:int}")]
        public async Task<IActionResult> MarkItemAsPacked([FromRoute] int idItem)
        {
            var res = await _itemDbRepository.MarkItemAsPacked(idItem);
            if (!res)
                return NotFound("Item with given id was not found");
            return Ok("Item is now packed");
        }
        
    }
}