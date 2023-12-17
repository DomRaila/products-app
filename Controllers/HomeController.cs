using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestApp.Models;
using Contentful.Core;
using Contentful.Core.Search;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace TestApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IContentfulClient _client;

    public HomeController(ILogger<HomeController> logger, IContentfulClient client)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        //var products = await _client.GetEntries<Product>();

        var qb = QueryBuilder<Product>.New.ContentTypeIs("product");
        var products = await _client.GetEntries(qb);
        var qb2 = QueryBuilder<Category>.New.ContentTypeIs("category");
        var categories = await _client.GetEntries(qb2);

        var viewModel = new ProductCategoryViewModel
        {
            Products = products,
            Categories = categories
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Content(string id)
    {
        var builder = new QueryBuilder<Product>().FieldEquals(f => f.Sys.Id, id).Include(2);
        var entry = (await _client.GetEntries(builder)).FirstOrDefault();

        return View(entry);
    }


    [HttpPost]
    public async Task<IActionResult> Index(string searchQuery, string categoryFilter)
    {
        // Create a query builder for the 'product' content type
        var productQuery = QueryBuilder<Product>.New.ContentTypeIs("product");

        // Apply search query filter if provided
        if (!string.IsNullOrEmpty(searchQuery))
        {
            productQuery = productQuery.FieldMatches("fields.name", searchQuery);
        }

        // Apply category filter if provided
        if (!string.IsNullOrEmpty(categoryFilter))
        {
            productQuery = productQuery.FieldEquals("fields.category.sys.id", categoryFilter);
        }

        // Retrieve products based on the constructed query
        var products = await _client.GetEntries(productQuery);

        // Create a query builder for the 'category' content type
        var categoryQuery = QueryBuilder<Category>.New.ContentTypeIs("category");

        // Retrieve all categories
        var categories = await _client.GetEntries(categoryQuery);

        // Create a view model to pass data to the view
        var viewModel = new ProductCategoryViewModel
        {
            Products = products,
            Categories = categories
        };

        // Return the view with the populated view model
        return View(viewModel);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
