using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EcoSwap.Models;
using System.Linq;
using System.Collections.Generic;

namespace EcoSwap.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var featuredProducts = GetMockProducts().Where(p => p.Tags.Contains("bestseller")).ToList();
        ViewBag.FeaturedProducts = featuredProducts;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Shop(string searchTerm, string category, string priceRange, string sortBy, string filter)
    {
        var products = GetMockProducts();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            products = products.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                                             p.Description.ToLower().Contains(searchTerm.ToLower())).ToList();
        }

        if (!string.IsNullOrEmpty(category))
        {
            products = products.Where(p => p.Category == category).ToList();
        }

        if (!string.IsNullOrEmpty(filter))
        {
            switch (filter)
            {
                case "bestseller":
                    products = products.Where(p => p.Tags.Contains("bestseller")).ToList();
                    break;
                case "new":
                    products = products.Where(p => p.Tags.Contains("new")).ToList();
                    break;
                case "local":
                    products = products.Where(p => p.Tags.Contains("local")).ToList();
                    break;
                case "high-impact":
                    products = products.Where(p => p.ImpactKg >= 5.0).ToList();
                    break;
                case "budget":
                    products = products.Where(p => p.Price <= 500).ToList();
                    break;
            }
        }

        if (!string.IsNullOrEmpty(priceRange))
        {
            switch (priceRange)
            {
                case "0-500":
                    products = products.Where(p => p.Price >= 0 && p.Price <= 500).ToList();
                    break;
                case "500-1000":
                    products = products.Where(p => p.Price > 500 && p.Price <= 1000).ToList();
                    break;
                case "1000-2000":
                    products = products.Where(p => p.Price > 1000 && p.Price <= 2000).ToList();
                    break;
                case "2000+":
                    products = products.Where(p => p.Price > 2000).ToList();
                    break;
            }
        }

        switch (sortBy)
        {
            case "price-low":
                products = products.OrderBy(p => p.Price).ToList();
                break;
            case "price-high":
                products = products.OrderByDescending(p => p.Price).ToList();
                break;
            case "rating":
                products = products.OrderByDescending(p => p.Rating).ToList();
                break;
            case "impact":
                products = products.OrderByDescending(p => p.ImpactKg).ToList();
                break;
            default:
                products = products.OrderByDescending(p => p.Tags.Contains("bestseller") ? 1 : 0).ToList();
                break;
        }

        ViewBag.CurrentSearch = searchTerm;
        ViewBag.CurrentCategory = category;
        ViewBag.CurrentPriceRange = priceRange;
        ViewBag.CurrentSort = sortBy ?? "featured";
        ViewBag.CurrentFilter = filter;

        return View(products);
    }

    public IActionResult Details(int id)
    {
        var product = GetMockProducts().FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    public IActionResult Admin()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult About()
    {
        return View();
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
                Tags = "bestseller,new,local"
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
                Tags = "bestseller,high-impact"
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