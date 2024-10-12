using InventoryWebApi.DTO;
using InventoryWebApi.Models;
using Newtonsoft.Json;

namespace InventoryWebApi.Mapper
{
    public static class InventoryMapper
    {
        public static InventoryItem MapToInventoryItem(this CreateForm createForm)
        {
            return new InventoryItem
            {
                Name = createForm.Name,
                Category = createForm.Category,
                Price = createForm.Price,
                Discount = createForm.Discount,
                Quantity = createForm.Quantity
            };
        }
        
        public static void UpdateInventoryItem(this CreateForm createForm, InventoryItem inventoryItem)
        {
                inventoryItem.Name = createForm.Name;
                inventoryItem.Category = createForm.Category;
                inventoryItem.Price = createForm.Price;
                inventoryItem.Discount = createForm.Discount;
                inventoryItem.Quantity = createForm.Quantity;
        }
    }
}
