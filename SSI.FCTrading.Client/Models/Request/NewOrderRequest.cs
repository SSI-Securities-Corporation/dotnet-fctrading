using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models
{
    public class NewOrderRequest
    {
        public string instrumentID { get; set; }
        public string market { get; set; }
        public string buySell { get; set; }
        public string orderType { get; set; }
        public string channelID { get; set; }
        public double price { get; set; }
        public long quantity { get; set; }
        public string account { get; set; }
        public string requestID { get; set; }
        public bool stopOrder { get; set; }
        public double stopPrice { get; set; }
        public string stopType { get; set; }
        public double stopStep { get; set; }
        public double lossStep { get; set; }
        public double profitStep { get; set; }
        public string code { get; set; }
        public bool modifiable { get; set; }

    }
}
