using Contentful.Core.Models;
using Contentful.Core.Models.Management;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TestApp.Models;

public class Category
{
    public SystemProperties Sys { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }    
}

public class Product
{
    public SystemProperties Sys { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Category Category { get; set; }
    public Asset? Image { get; set; }
}

public class ProductCategoryViewModel
{
    public IEnumerable<Product> Products { get; set; }
    public IEnumerable<Category> Categories { get; set; }
}
