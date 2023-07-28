using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Response
{

    public class Data
    {
        public string account { get; set; }
        public int maxBuyQty { get; set; }
        public string marginRatio { get; set; }
        public int purchasingPower { get; set; }
    }

    public class MaxBuyQuantityAccountResponse
    {
        public string message { get; set; }
        public int status { get; set; }
        public Data data { get; set; }
    }


}
