namespace BizBulkApp.Models
{
    public class Quotation
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }  // Buyer requesting the quotation
        public int SellerId { get; set; } // Seller responding to the quotation
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } // Pending, Approved, Rejected
        public string Comments { get; set; } // Any remarks from the buyer/seller

        public User Buyer { get; set; }
        public User Seller { get; set; }

        public int quotationTrackNumber { get; set; }
        public ICollection<QuotationItem> QuotationItems { get; set; }
    }

}
