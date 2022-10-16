using System;
using System.Collections.Generic;

#nullable disable

namespace RestourantPrinter.Models
{
    public partial class OrderDetails
    {
        public OrderDetails()
        {
            OrderDetailProps = new List<OrderDetailProp>();
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public double OrderMenuPrice { get; set; }
        public string OrderMenuTitle { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }

        public virtual OrderHeader Order { get; set; }
        public virtual List<OrderDetailProp> OrderDetailProps { get; set; }
    }
}
