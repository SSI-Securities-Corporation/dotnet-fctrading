using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Response
{

    public class CancelOrderData
    {
        public string orderID { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Account { get; set; }
        public string InstrumentID { get; set; }
        public string MarketID { get; set; }
        public string BuySell { get; set; }
        public string orderType { get; set; }
    }

    public class ModifyRequestData
    {
        public string RequestID { get; set; }

        public CancelOrderData RequestData { get; set; }
    }
    public class ModifyOrderResponse
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public ModifyRequestData Data { get; set; }

    }
}
