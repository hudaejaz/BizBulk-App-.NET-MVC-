using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BizBulkApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace BizBulkApp.Controllers
{
    public class SellerController : Controller
    {
        private readonly AppDbContext _context;

        public SellerController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ReceivedQuotations()
        {
            // Fetch all quotations
            var quotations = await _context.Quotations
                .Include(q => q.Buyer) // Include buyer details
                .Include(q => q.QuotationItems)
                .ThenInclude(qi => qi.Product) // Include products in the quotation
                .ToListAsync();

            return View(quotations);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmQuotationSeller(int quotationId)
        {
            var quotation = await _context.Quotations.FindAsync(quotationId);
            if (quotation != null && quotation.Status != "Confirmed")
            {
                quotation.Status = "Confirmed";
                _context.Update(quotation);
                await _context.SaveChangesAsync();
            }
            return View();
        }
    }
}
