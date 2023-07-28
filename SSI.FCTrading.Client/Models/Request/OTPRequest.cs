using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Request
{
    public class OTPRequest
    {
        public string ConsumerID { get; set; }
        public string ConsumerSecret { get; set; }
    }
}
