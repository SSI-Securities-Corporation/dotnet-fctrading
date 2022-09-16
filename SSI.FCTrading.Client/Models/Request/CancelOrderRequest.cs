using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models
{
    public class CancelOrderRequest
    {
        public string OrderID { get; set; }
        public string Account { get; set; }
        public string MarketID { get; set; }
        public string InstrumentID { get; set; }
        public string BuySell { get; set; }
        public string RequestID { get; set; }
        public string Code { get; set; }
    }
}
