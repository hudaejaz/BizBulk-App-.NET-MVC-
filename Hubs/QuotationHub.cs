using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BizBulkApp.Hubs
{
    public class QuotationHub : Hub
    {
        public async Task UpdateQuotationStatus(int quotationId, string status)
        {
            await Clients.All.SendAsync("QuotationStatusUpdated", quotationId, status);
        }

        public async Task UpdateCart(int cartCount)
        {
            await Clients.All.SendAsync("UpdateCart", cartCount);
        }

        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
