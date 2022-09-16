using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Request
{
    public class MaxSellQuantityRequest
    {
        public string Account { get; set; }
        public string InstrumentID { get; set; }
        public decimal Price { get; set; }
    }
}