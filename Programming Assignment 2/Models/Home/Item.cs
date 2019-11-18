using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Programming_Assignment_2.Models.Home
{
    public class Item
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Total { get; set; }
    }
}