using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Request
{
    public class ModifyOrderRequest
    {
        public string OrderID { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Account { get; set; }
        public string InstrumentID { get; set; }
        public string MarketID { get; set; }
        public string BuySell { get; set; }
        public string RequestID { get; set; }
        public string OrderType { get; set; }
        public string Code { get; set; }
    }
}
