using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestApp.Models;
using Contentful.Core;
using Contentful.Core.Search;

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
        var entries = await _client.GetEntries(qb);

        return View(entries);
    }

    public async Task<IActionResult> Content(string id)
    {
        //var products = await _client.GetEntries<Product>();

        var qb = QueryBuilder<Product>.New.ContentTypeIs("product").FieldEquals(f => f.Sys.Id, id);
        var entry = await _client.GetEntries(qb);

        return View(entry);
    }


    [HttpPost]
    public async Task<IActionResult> Index(string searchQuery)
    {
        var products = await _client.GetEntries<Product>();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            //Filter products based on the searchQuery
            //products = products.Where(p => p.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
        }

        return View(products.ToList());
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
