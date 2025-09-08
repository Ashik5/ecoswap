using Microsoft.AspNetCore.Mvc;
using EcoSwap.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EcoSwap.Controllers
{
    public class CartController : Controller
    {
        private readonly List<Product> _products;

        public CartController()
        {
            // In a real application, you would get the products from a database.
            _products = GetMockProducts();
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                var cart = GetCart();
                cart.AddItem(product, quantity);
                SaveCart(cart);
                return Json(new { itemCount = cart.Items.Sum(i => i.Quantity) });
            }
            return Json(new { error = "Product not found" });
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();
            cart.RemoveItem(id);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var cart = GetCart();
            cart.UpdateQuantity(id, quantity);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        private Cart GetCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (cartJson == null)
            {
                return new Cart();
            }
            return JsonConvert.DeserializeObject<Cart>(cartJson);
        }

        private void SaveCart(Cart cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("Cart", cartJson);
        }

        private List<Product> GetMockProducts()
        {
            return new List<Product>
            {
                new Product {
                    Id = 1,
                    Name = "Bamboo Toothbrush Set",
                    Category = "Personal Care",
                    Description = "Set of 4 biodegradable bamboo toothbrushes with soft bristles",
                    Price = 450,
                    OriginalPrice = 600,
                    Rating = 4.8,
                    ReviewCount = 234,
                    ImpactKg = 0.8,
                    ImageFileName = "1.jpg",
                    Tags = "bestseller,eco-friendly"
                },
                new Product {
                    Id = 2,
                    Name = "Stainless Steel Water Bottle",
                    Category = "Kitchen",
                    Description = "Insulated 750ml bottle keeps drinks cold for 24h, hot for 12h",
                    Price = 1250,
                    OriginalPrice = null,
                    Rating = 4.9,
                    ReviewCount = 567,
                    ImpactKg = 12.5,
                    ImageFileName = "2.jpg",
                    Tags = "bestseller,high-impact"
                },
                new Product {
                    Id = 3,
                    Name = "Organic Shampoo Bar",
                    Category = "Personal Care",
                    Description = "Chemical-free solid shampoo bar for all hair types, lasts 80+ washes",
                    Price = 320,
                    OriginalPrice = null,
                    Rating = 4.6,
                    ReviewCount = 189,
                    ImpactKg = 2.1,
                    ImageFileName = "3.jpg",
                    Tags = "new,local"
                },
                new Product {
                    Id = 4,
                    Name = "Reusable Food Containers",
                    Category = "Kitchen",
                    Description = "Set of 5 glass containers with airtight bamboo lids",
                    Price = 2100,
                    OriginalPrice = 2800,
                    Rating = 4.7,
                    ReviewCount = 123,
                    ImpactKg = 8.3,
                    ImageFileName = "4.jpg",
                    Tags = "high-impact"
                },
                new Product {
                    Id = 5,
                    Name = "Natural Dish Soap",
                    Category = "Cleaning",
                    Description = "Plant-based concentrated formula, refillable bottle",
                    Price = 275,
                    OriginalPrice = null,
                    Rating = 4.5,
                    ReviewCount = 98,
                    ImpactKg = 1.2,
                    ImageFileName = "4.jpg",
                    Tags = "budget,local"
                },
                new Product {
                    Id = 6,
                    Name = "Recycled Paper Notebooks",
                    Category = "Office",
                    Description = "Pack of 3 notebooks made from 100% recycled paper",
                    Price = 380,
                    OriginalPrice = null,
                    Rating = 4.4,
                    ReviewCount = 76,
                    ImpactKg = 0.6,
                    ImageFileName = "5.jpg",
                    Tags = "budget,eco-friendly"
                },
                new Product {
                    Id = 7,
                    Name = "Coconut Fiber Sponges",
                    Category = "Cleaning",
                    Description = "Natural scrubbing sponges, pack of 6, compostable",
                    Price = 195,
                    OriginalPrice = 250,
                    Rating = 4.3,
                    ReviewCount = 145,
                    ImpactKg = 0.4,
                    ImageFileName = "6.jpg",
                    Tags = "budget,new"
                },
                new Product {
                    Id = 8,
                    Name = "Hemp Tote Bag",
                    Category = "Fashion",
                    Description = "Durable shopping bag with reinforced handles, machine washable",
                    Price = 650,
                    OriginalPrice = null,
                    Rating = 4.9,
                    ReviewCount = 312,
                    ImpactKg = 15.2,
                    ImageFileName = "7.jpg",
                    Tags = "bestseller,high-impact"
                }
            };
        }
    }
}
