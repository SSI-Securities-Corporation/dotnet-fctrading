using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Response
{

    //public class OrderBookResponse
    //{
    //    public string message { get; set; }
    //    public int status { get; set; }
    //    public OrderBookData data { get; set; }
    //}

    public class OrderBookResponse
    {
        public string account { get; set; }
        public List<OrderBook> orders { get; set; }
    }

  

    public class OrderBook
    {
        public string uniqueID;
        public string orderID;
        public string buySell;
        public double price;
        public long quantity;
        public long filledQty;
        public string orderStatus;
        public string marketID;
        public string inputTime;
        public string modifiedTime;
        public string instrumentID;
        public string orderType;
        public long cancelQty;
        public double avgPrice;
        public string isForcesell;
        public string isShortsell;
        public string rejectReason = "";
    }

}
