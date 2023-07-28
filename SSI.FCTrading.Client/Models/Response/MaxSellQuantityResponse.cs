using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Response
{
    public class MaxSellQuantityResponse
    {
        public string message { get; set; }
        public int status { get; set; }
        public MaxSellQuantityData data { get; set; }
    }
    public class MaxSellQuantityData
    {
        public string account { get; set; }
        public int maxSellQty { get; set; }
    }

}
