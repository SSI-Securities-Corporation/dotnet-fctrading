using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Response
{
    //public class DerivativePositionResponse
    //{
    //    public string message { get; set; }
    //    public int status { get; set; }
    //    public DerivativePositionData data { get; set; }
    //}

    public class ClosePosition
    {
        public string marketID { get; set; }
        public string instrumentID { get; set; }
        public int longQty { get; set; }
        public int shortQty { get; set; }
        public int net { get; set; }
        public int bidAvgPrice { get; set; }
        public int askAvgPrice { get; set; }
        public int tradePrice { get; set; }
        public int marketPrice { get; set; }
        public int floatingPL { get; set; }
        public int tradingPL { get; set; }
    }

    public class DerivativePositionResponse
    {
        public string account { get; set; }
        public List<OpenPosition> openPosition { get; set; }
        public List<ClosePosition> closePosition { get; set; }
    }

    public class OpenPosition
    {
        public string marketID { get; set; }
        public string instrumentID { get; set; }
        public int longQty { get; set; }
        public int shortQty { get; set; }
        public int net { get; set; }
        public int bidAvgPrice { get; set; }
        public int askAvgPrice { get; set; }
        public int tradePrice { get; set; }
        public int marketPrice { get; set; }
        public int floatingPL { get; set; }
        public int tradingPL { get; set; }
    }

  


}
