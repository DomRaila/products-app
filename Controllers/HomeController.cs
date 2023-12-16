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
        var products = await _client.GetEntries<Product>();

        //foreach (var product in products)
        //{
        //}

        return View(products);
    }

    [HttpPost]
    public IActionResult HandleForm(string productName, string productDescription, decimal productPrice)
    {
        // Handle the form data here
        // You can perform any necessary processing with the form data

        // Example: Log the form data
        Console.WriteLine($"Received form data: Name={productName}, Description={productDescription}, Price={productPrice}");

        // Redirect or return a response as needed
        return RedirectToAction("Index");
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
