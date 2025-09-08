using System.Collections.Generic;
using System.Linq;

namespace EcoSwap.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(Product product, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Product.Id == product.Id);
            if (item == null)
            {
                Items.Add(new CartItem { Product = product, Quantity = quantity });
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void RemoveItem(int productId)
        {
            Items.RemoveAll(i => i.Product.Id == productId);
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Product.Id == productId);
            if (item != null)
            {
                item.Quantity = quantity;
            }
        }

        public decimal GetTotal()
        {
            return (decimal)Items.Sum(i => i.Product.Price * i.Quantity);
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}
