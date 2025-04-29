using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using BizBulkApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizBulkApp.Hubs;

namespace BizBulkApp.Controllers
{
    public class QuotationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<QuotationHub> _hubContext;

        public QuotationController(AppDbContext context, IHubContext<QuotationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            var quotations = _context.Quotations.Include(q => q.Buyer).Include(q => q.Seller).ToList();
            return View(quotations);
        }

        public IActionResult SubmitQuotation()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotation = await _context.Quotations
                .Include(q => q.Buyer)
                .Include(q => q.Seller)
                .Include(q => q.QuotationItems)
                    .ThenInclude(qi => qi.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (quotation == null)
            {
                return NotFound();
            }

            return View(quotation);
        }

        [HttpPost]
        public async Task<IActionResult> QuotationForm()
        {
            Random random = new Random();
            int quotationId = random.Next(1000, 9999);

            var buyerId = int.Parse(HttpContext.Session.GetString("UserId"));

            var order = _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.UserId == buyerId && o.Status == "Pending");

            if (order == null)
            {
                TempData["ErrorMessage"] = "No pending order found for the user.";
                return RedirectToAction("Index", "Home");
            }

            int sellerId = 1;

            Quotation quotation = new Quotation()
            {
                quotationTrackNumber = quotationId,
                BuyerId = buyerId,
                SellerId = sellerId,
                CreatedDate = DateTime.Now,
                Status = "Pending",
                Comments = "Quotation generated for buyer",
                QuotationItems = new List<QuotationItem>()
            };

            foreach (var orderItem in order.OrderItems)
            {
                var quotationItem = new QuotationItem()
                {
                    ProductId = orderItem.ProductId,
                    Quantity = orderItem.Quantity,
                    Price = orderItem.Price
                };
                quotation.QuotationItems.Add(quotationItem);
            }

            _context.Quotations.Add(quotation);
            await _context.SaveChangesAsync();

            // Notify clients about the new quotation
            await _hubContext.Clients.All.SendAsync("ReceiveQuotationUpdate", quotationId, "New Quotation Created!");

            TempData["SuccessMessage"] = $"Quotation successfully created! Quotation ID: {quotationId}";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmQuotation(int quotationId)
        {
            var quotation = _context.Quotations
                                    .Include(q => q.QuotationItems)
                                    .ThenInclude(qi => qi.Product)
                                    .FirstOrDefault(q => q.quotationTrackNumber == quotationId);

            if (quotation == null)
            {
                TempData["ErrorMessage"] = "Quotation not found.";
                return RedirectToAction(nameof(Index));
            }

            if (quotation.Status != "Pending")
            {
                TempData["ErrorMessage"] = "Quotation cannot be confirmed, as it is not in a pending state.";
                return RedirectToAction(nameof(Index));
            }

            quotation.Status = "Confirmed";
            _context.Update(quotation);

            var order = _context.Orders
                                .Include(o => o.OrderItems)
                                .FirstOrDefault(o => o.UserId == quotation.BuyerId && o.Status == "Pending");

            if (order != null)
            {
                _context.OrderItems.RemoveRange(order.OrderItems);
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();

            // Notify clients about the confirmed quotation
            await _hubContext.Clients.All.SendAsync("ReceiveQuotationUpdate", quotationId, "Quotation Confirmed!");

            TempData["SuccessMessage"] = "Quotation confirmed successfully and cart cleared.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult TrackQuotation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TrackQuotation(int quotationTrackNumber)
        {
            var quotation = _context.Quotations
                .Include(q => q.Buyer)
                .Include(q => q.Seller)
                .Include(q => q.QuotationItems)
                .FirstOrDefault(q => q.quotationTrackNumber == quotationTrackNumber);

            if (quotation == null)
            {
                return Json(new { success = false, message = "Quotation not found." });
            }

            return PartialView("_QuotationDetails", quotation);
        }
    }
}
