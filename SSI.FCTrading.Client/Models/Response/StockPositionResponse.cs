using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Response
{

    //public class StockPositionResponse
    //{
    //    public string message { get; set; }
    //    public int status { get; set; }
    //    public StockPositionData data { get; set; }
    //}

    public class StockPositionResponse
    {
        public string account { get; set; }
        public long totalMarketValue { get; set; }
        public List<StockPosition> stockPositions { get; set; }
    }

  

    public class StockPosition
    {
        public string marketID { get; set; }
        public string instrumentID { get; set; }
        public long onHand { get; set; }
        public long block { get; set; }
        public long bonus { get; set; }
        public long buyT0 { get; set; }
        public long buyT1 { get; set; }
        public long buyT2 { get; set; }
        public long sellT0 { get; set; }
        public long sellT1 { get; set; }
        public long sellT2 { get; set; }
        public long avgPrice { get; set; }
        public long mortgage { get; set; }
        public long sellableQty { get; set; }
        public long holdForTrade { get; set; }
        public long marketPrice { get; set; }
    }

}
