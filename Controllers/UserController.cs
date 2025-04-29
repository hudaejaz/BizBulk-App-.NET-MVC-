using BizBulkApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class UserController : Controller
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }   

    // Register method for creating new user
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(User model, string Role)
    {
        if (ModelState.IsValid)
        {
            // Check if the user already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "User already exists.");
                return View(model);
            }

            // Assign role if not already set (default to "Buyer")
            model.Role = string.IsNullOrEmpty(Role) ? "Buyer" : Role;

            // Add user to the database with plain text password
            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            // Authenticate and log the user in after registration
            await Authenticate(model);  // Implement your authentication logic here

            // Redirect to Home page after successful registration
            return RedirectToAction("Index", "Home");
        }

        // If model validation fails, return the same view with validation errors
        return View(model);
    }

    // Login method
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        // Find the user by email
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null || user.Password != password)
        {
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        // Authenticate the user
        await Authenticate(user);

        // Redirect based on role
        if (user.Role == "Seller")
        {
            return RedirectToAction("ReceivedQuotations", "Seller");
        }

        // Default to Home for buyers
        return RedirectToAction("Index", "Home");
    }

    // Logout method
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Remove("UserEmail");
        HttpContext.Session.Remove("UserRole");
        return RedirectToAction("Login", "User");
    }

    // Helper method to create session
    private async Task Authenticate(User user)
    {
        // Store user info in session

        HttpContext.Session.SetString("UserEmail", user.Email);
        HttpContext.Session.SetString("UserRole", user.Role);
        HttpContext.Session.SetString("UserId",user.Id.ToString());
    }

    // Access Denied if not authorized
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        return View();
    }
}
