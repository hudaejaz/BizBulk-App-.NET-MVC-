namespace BizBulkApp.Models
{
    public class QuotationItem
    {
        public int Id { get; set; }
        public int QuotationId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Proposed price per unit by the seller

        public Quotation Quotation { get; set; }
        public Product Product { get; set; }
    }

}
