using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Response
{
    public class CancelData
    {
        public string OrderID { get; set; }
        public string Account { get; set; }
        public string MarketID { get; set; }
        public string InstrumentID { get; set; }
        public string BuySell { get; set; }
        public string RequestID { get; set; }
    }
    public class CancelRequestData
    {
        public string RequestID { get; set; }
        public CancelData RequestData { get; set; }
    }
    public class CancelOrderResponse
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public CancelRequestData Data { get; set; }


    }
}
