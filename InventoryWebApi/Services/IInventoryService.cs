using InventoryWebApi.DTO;
using InventoryWebApi.Models;

namespace InventoryWebApi.Services
{
    public interface IInventoryService
    {
        Task<List<InventoryItem>> GetItems();
        Task<InventoryItem> CreateItem(CreateForm createForm);
        Task<List<InventoryItem>> GetItemsByQuery(ItemQueryModel queryParameters);
        Task<List<InventoryItem>> GetSortedItems();
        Task UpdateItem(int barcode, CreateForm updateForm);
        Task DeleteItem(int barcode);
    }
}
