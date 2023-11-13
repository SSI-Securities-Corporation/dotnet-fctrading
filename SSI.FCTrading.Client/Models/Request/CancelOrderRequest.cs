using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models
{
    public class CancelOrderRequest
    {
        public String orderID;
        public String account;
        public String marketID;
        public String instrumentID;
        public String buySell;
        public String requestID;
        public String code;
        public String deviceId;
        public String userAgent;
    }

    public class DerCancelOrderRequest
    {
        public String orderID;
        public String account;
        public String marketID;
        public String instrumentID;
        public String buySell;
        public String requestID;
        public String code;
        public String deviceId;
        public String userAgent;
    }
}
