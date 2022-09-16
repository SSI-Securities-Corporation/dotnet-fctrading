using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Response
{

    public class StockPositionResponse
    {
        public string message { get; set; }
        public int status { get; set; }
        public StockPositionData data { get; set; }
    }

    public class StockPositionData
    {
        public string account { get; set; }
        public int totalMarketValue { get; set; }
        public List<StockPosition> stockPositions { get; set; }
    }

  

    public class StockPosition
    {
        public string marketID { get; set; }
        public string instrumentID { get; set; }
        public int onHand { get; set; }
        public int block { get; set; }
        public int bonus { get; set; }
        public int buyT0 { get; set; }
        public int buyT1 { get; set; }
        public int buyT2 { get; set; }
        public int sellT0 { get; set; }
        public int sellT1 { get; set; }
        public int sellT2 { get; set; }
        public int avgPrice { get; set; }
        public int mortgage { get; set; }
        public int sellableQty { get; set; }
        public int holdForTrade { get; set; }
        public int marketPrice { get; set; }
    }

}
