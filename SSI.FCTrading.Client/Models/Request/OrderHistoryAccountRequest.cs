using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Request
{
    public class OrderHistoryAccountRequest
    {
        public string account { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }         
    }
}
