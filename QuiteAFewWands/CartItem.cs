using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuiteAFewWands
{
    public class CartItem
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public float ItemPrice { get; set; }
        public string ItemName { get; set; }
    }
}