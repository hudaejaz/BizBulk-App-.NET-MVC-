﻿namespace BizBulkApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }

        public string ImageUrl { get; set; }
    }

}
