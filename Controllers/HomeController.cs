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
        //var products = await _client.GetEntries<Product>();

        var qb = QueryBuilder<Product>.New.ContentTypeIs("product").FieldEquals(f => f.Sys.Id, id);
        var entry = await _client.GetEntries(qb);

        return View(entry);
    }


    [HttpPost]
    public async Task<IActionResult> Index(string searchQuery, string categoryFilter)
    {
        var productQuery = QueryBuilder<Product>.New.ContentTypeIs("product");

        if (!string.IsNullOrEmpty(searchQuery))
        {
            productQuery = productQuery.FieldMatches("fields.name", searchQuery);
        }

        if (!string.IsNullOrEmpty(categoryFilter))
        {
            productQuery = productQuery.FieldEquals("fields.category.sys.id", categoryFilter);
        }

        var products = await _client.GetEntries(productQuery);

        var categoryQuery = QueryBuilder<Category>.New.ContentTypeIs("category");
        var categories = await _client.GetEntries(categoryQuery);

        var viewModel = new ProductCategoryViewModel
        {
            Products = products,
            Categories = categories
        };

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
