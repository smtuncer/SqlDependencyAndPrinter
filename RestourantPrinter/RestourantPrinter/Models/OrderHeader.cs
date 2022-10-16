using System;
using System.Collections.Generic;

#nullable disable

namespace RestourantPrinter.Models
{
    public partial class OrderHeader
    {
        public OrderHeader()
        {
            OrderDetails = new List<OrderDetails>();
        }

        public int id { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderTotal { get; set; }
        public string OrderStatus { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string SummaryNote { get; set; }
        public string PaymentMethod { get; set; }

        public virtual AspNetUser ApplicationUser { get; set; }
        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
