using InventoryWebApi.DataAccess;
using InventoryWebApi.DTO;
using InventoryWebApi.Mapper;
using InventoryWebApi.Models;

namespace InventoryWebApi.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IRepository _repository;

        public InventoryService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<InventoryItem> CreateItem(CreateForm createForm)
        {
            var inventoryItem = createForm.MapToInventoryItem();
            return await _repository.AddInventoryItem(inventoryItem);
        }

        public async Task DeleteItem(int barcode)
        {
            var inventoryItem = new ItemQueryModel
            {
                Barcode = barcode
            };
            
            var items = await _repository.GetInventoryItem(inventoryItem);
            var itemToDelete = items.FirstOrDefault();
            if (itemToDelete == null)
            {
                return;
            }
            await _repository.DeleteInventoryItem(itemToDelete);
        }

    public async Task<List<InventoryItem>> GetItems()
        {
           return await _repository.GetInventory();
        }

        public async Task<List<InventoryItem>> GetItemsByQuery(ItemQueryModel queryParameters)
        {
            return await _repository.GetInventoryItem(queryParameters);
        }

        public async Task<List<InventoryItem>> GetSortedItems()
        {
            return await _repository.GetSorted();
        }

        public async Task UpdateItem(int barcode, CreateForm updateForm)
        {
            var query = new ItemQueryModel
            {
                Barcode = barcode
            };
            var inventoryItem = await _repository.GetInventoryItem(query);
            var itemPresent  = inventoryItem.FirstOrDefault();
            if (itemPresent == null)
            {
                throw new KeyNotFoundException("Item not found");
            }
            await _repository.UpdateInventoryItem(itemPresent);
        }
    }

}
