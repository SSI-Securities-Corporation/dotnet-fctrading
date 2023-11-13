using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Request
{
    public class ModifyOrderRequest
    {
        public String orderID;
        public double price;
        public long quantity;
        public String account;
        public String instrumentID;
        public String marketID;
        public String buySell;
        public String requestID;
        public String orderType;
        public String code;
        public String deviceId;
        public String userAgent;
    }

    public class DerModifyOrderRequest
    {
        public String orderID;
        public double price;
        public long quantity;
        public String account;
        public String instrumentID;
        public String marketID;
        public String buySell;
        public String requestID;
        public String orderType;
        public String code;
        public String deviceId;
        public String userAgent;
    }
}
