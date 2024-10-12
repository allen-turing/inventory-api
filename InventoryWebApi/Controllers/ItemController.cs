using InventoryWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using InventoryWebApi.DTO;
using InventoryWebApi.Services;

namespace InventoryWebApi.Controllers
{
    [ApiController]
    [Route("inventory/item")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddItemsAsync(CreateForm request)
        {
            var itemCreated = await _inventoryService.CreateItem(request);
            return Created("Item Created", itemCreated);
        }

        [HttpDelete("{barcode}")]
        public async Task<IActionResult> DeleteItemByBarCodeAsync([FromRoute] int barcode)
        {
            await _inventoryService.DeleteItem(barcode);
            return NoContent();
        }

        [HttpPut("{barcode}")]
        public async Task<IActionResult> UpdateItemByBarCodeAsync([FromRoute] int barcode, 
        [FromBody] CreateForm request)
        {
            try
            {
                await _inventoryService.UpdateItem(barcode,request);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItemsAsync()
        {
            var allItems =  await _inventoryService.GetItems();
            return Ok(allItems);
        }

        [HttpGet("query")]
        public async Task<IActionResult> GetAllItemsByCategoryAsync([FromQuery] string? category,
            [FromQuery] int? barcode,
            [FromQuery] int? discount,
            [FromQuery] string? name)
        {
            var itemQueryModel = new ItemQueryModel()
            {
                Category = category,
                Barcode = barcode,
                Discount = discount,
                Name = name
            };
            var allItems = await _inventoryService.GetItemsByQuery(itemQueryModel);
            if(allItems is null)
            {
                var emptyArray = Array.Empty<InventoryItem>();
                return Ok(emptyArray);
            }
            return Ok(allItems);
        }

        [HttpGet("sort")]
        public async Task<IActionResult> GetAllItemsByPriceDescendingAsync()
        {
            var allItems = await _inventoryService.GetSortedItems();
            return Ok(allItems);
        }
    }

}
