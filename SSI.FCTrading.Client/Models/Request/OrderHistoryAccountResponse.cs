using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Request
{
    public class OrderHistoryAccountResponse
    {
        public string message { get; set; }
        public int status { get; set; }
        public Data data { get; set; }
    }
    public class Data
    {
        public List<OrderHistory> orderHistories { get; set; }
        public string account { get; set; }
    }

    public class OrderHistory
    {
        public string uniqueID { get; set; }
        public string orderID { get; set; }
        public string buySell { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public int filledQty { get; set; }
        public string orderStatus { get; set; }
        public string marketID { get; set; }
        public string inputTime { get; set; }
        public string modifiedTime { get; set; }
        public string instrumentID { get; set; }
        public string orderType { get; set; }
        public int cancelQty { get; set; }
        public int avgPrice { get; set; }
        public string isForcesell { get; set; }
        public string isShortsell { get; set; }
    }

 


}
