using Contentful.Core.Models;
using Contentful.Core.Models.Management;

namespace TestApp.Models;

public class Category
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public class Product
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Reference? Category { get; set; }
    public Asset? Image { get; set; }
}