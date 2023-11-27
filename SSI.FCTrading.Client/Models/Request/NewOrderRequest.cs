using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models
{
    public class NewOrderRequest
    {
        public string instrumentID;
        public string market;
        public string buySell;
        public string orderType;
        public string channelID;
        public double price;
        public int quantity;
        public string account;
        public string requestID;
        public bool stopOrder = false;
        public double stopPrice;
        public string stopType;
        public double stopStep;
        public double lossStep;
        public double profitStep;
        public string code;
        public bool modifiable = true;

        public string deviceId;
        public string userAgent;

    }

    public class DerNewOrderRequest
    {
        public string instrumentID;
        public string market;
        public string buySell;
        public string orderType;
        public string channelID;
        public double price;
        public int quantity;
        public string account;
        public string requestID;
        public bool stopOrder = false;
        public double stopPrice;
        public string stopType;
        public double stopStep;
        public double lossStep;
        public double profitStep;
        public string code;
        public bool modifiable = true;

        public string deviceId;
        public string userAgent;

    }
}
