using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Request
{
    public class MaxSellQuantityRequest
    {
        public string account { get; set; }
        public string instrumentID { get; set; }
        public decimal price { get; set; }
    }
}