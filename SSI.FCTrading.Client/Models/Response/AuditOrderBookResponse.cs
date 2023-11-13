using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Response
{

    //public class AuditOrderBookResponse
    //{
    //    public string message { get; set; }
    //    public int status { get; set; }
    //    public AuditOrderBookData data { get; set; }
    //}

    public class AuditOrderBookResponse
    {
        public string account { get; set; }
        public List<AuditOrderBook> orders { get; set; }
    }

  

    public class AuditOrderBook
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
        public AuditOrderBook lastErrorEvent = null;
    }

}
