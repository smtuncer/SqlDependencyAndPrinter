using System;
using System.Collections.Generic;

#nullable disable

namespace RestourantPrinter.Models
{
    public partial class OrderDetailProp
    {
        public int OrderDetailPropId { get; set; }
        public int? Id { get; set; }
        public double OrderProductPropertiesPrice { get; set; }
        public string OrderProductPropertiesTitle { get; set; }

        public virtual OrderDetails IdNavigation { get; set; }
    }
}
