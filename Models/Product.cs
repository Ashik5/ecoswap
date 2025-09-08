namespace EcoSwap.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int? OriginalPrice { get; set; }
    public double Rating { get; set; }
    public int ReviewCount { get; set; }
    public double ImpactKg { get; set; }
    public string ImageFileName { get; set; }
    public string Tags { get; set; }
}