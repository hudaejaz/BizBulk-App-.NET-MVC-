using BizBulkApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CartController : Controller
{
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId)
    {
        // Ensure the user is authenticated
        var userEmail = HttpContext.Session.GetString("UserEmail");
        if (string.IsNullOrEmpty(userEmail))
        {
            return RedirectToAction("Login", "User");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == userEmail);

        if (user == null)
        {
            return RedirectToAction("Login", "User");
        }

        // Get the product
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == productId);

        if (product == null)
        {
            return NotFound("Product not found");
        }

        // Check if there is an existing cart (Order with status 'Pending') for the user
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.UserId == user.Id && o.Status == "Pending");

        if (order == null)
        {
            // Create a new order
            order = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.Now,
                TotalAmount = 0,
                Status = "Pending",
                OrderItems = new List<OrderItem>()
            };

            _context.Orders.Add(order);
        }

        // Check if the product is already in the cart
        var orderItem = order.OrderItems
            .FirstOrDefault(oi => oi.ProductId == productId);

        if (orderItem == null)
        {
            // Add new order item to the cart
            orderItem = new OrderItem
            {
                ProductId = product.Id,
                Quantity = 1,
                Price = product.Price
            };

            order.OrderItems.Add(orderItem);
        }
        else
        {
            // Update the quantity and price of the existing order item
            orderItem.Quantity += 1;
            orderItem.Price = product.Price;
        }

        // Update the total amount of the order
        order.TotalAmount = order.OrderItems.Sum(oi => oi.Quantity * oi.Price);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Cart()
    {
        var userEmail = HttpContext.Session.GetString("UserEmail");
        if (string.IsNullOrEmpty(userEmail))
        {
            return RedirectToAction("Login", "User");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == userEmail);

        if (user == null)
        {
            return RedirectToAction("Login", "User");
        }

        // Get the user's cart
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.UserId == user.Id && o.Status == "Pending");

        if (order == null)
        {
            // Create an empty cart
            order = new Order
            {
                OrderItems = new List<OrderItem>()
            };
        }

        return View(order);
    }

    [HttpGet]
    public async Task<IActionResult> ViewCart()
    {
        var userEmail = HttpContext.Session.GetString("UserEmail");
        if (string.IsNullOrEmpty(userEmail))
        {
            return RedirectToAction("Login", "User");
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == userEmail);

        if (user == null)
        {
            return RedirectToAction("Login", "User");
        }

        // Get the user's cart
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.UserId == user.Id && o.Status == "Pending");

        if (order == null)
        {
            // Create an empty cart
            order = new Order
            {
                OrderItems = new List<OrderItem>()
            };
        }

        return View(order);
    }

    
}
