using System.Diagnostics;
using BizBulkApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BizBulkApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Search(string category, string searchTerm)
        {
            IEnumerable<Product> results;

            // Filter based on category and search term
            if (category == "0" || string.IsNullOrEmpty(category))
            {
                // All categories
                results = _context.Products
                    .Where(p => p.Name.Contains(searchTerm))
                    .ToList();
            }
            else
            {
                // Specific category
                results = _context.Products
                    .Where(p => p.Category == category && p.Name.Contains(searchTerm))
                    .ToList();
            }

            // Return a partial view with the search results
            return PartialView("_SearchResults", results);
        }

        public async Task<IActionResult> Index()
        {
            // Fetch products from the database
            var products = await _context.Products.ToListAsync();

            // Pass the products list to the view
            return View(products);
        }
        public async Task<IActionResult> Accessories()
        {
            // Fetch products from the database where category is 'Accessories'
            var products = await _context.Products
                                          .Where(p => p.Category == "Accessories")
                                          .ToListAsync();

            // Pass the filtered products list to the view
            return View(products);
        }

        public async Task<IActionResult> Hotdeals()
        {
            // Fetch products from the database where category is 'Accessories'
            var products = await _context.Products
                                          .Where(p => p.Category == "Hotdeals")
                                          .ToListAsync();

            // Pass the filtered products list to the view
            return View(products);
        }
        public async Task<IActionResult> Phones()
        {
            // Fetch products from the database where category is 'Accessories'
            var products = await _context.Products
                                          .Where(p => p.Category == "Mobile Phones")
                                          .ToListAsync();

            // Pass the filtered products list to the view
            return View(products);
        }
        public async Task<IActionResult> Cameras()
        {
            // Fetch products from the database where category is 'Accessories'
            var products = await _context.Products
                                          .Where(p => p.Category == "Cameras")
                                          .ToListAsync();

            // Pass the filtered products list to the view
            return View(products);
        }

        public async Task<IActionResult> HomeAppliances()
        {
            // Fetch products from the database where category is 'Accessories'
            var products = await _context.Products
                                          .Where(p => p.Category == "Home Appliances")
                                          .ToListAsync();

            // Pass the filtered products list to the view
            return View(products);
        }
        public async Task<IActionResult> Laptops()
        {
            // Fetch products from the database where category is 'Accessories'
            var products = await _context.Products
                                          .Where(p => p.Category == "Laptops")
                                          .ToListAsync();

            // Pass the filtered products list to the view
            return View(products);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult TermsAndConditions()
        {
            return View();
        }
        public ActionResult OrderAndReturns()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
