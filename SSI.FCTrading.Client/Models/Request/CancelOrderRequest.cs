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
        [JsonProperty("orderID")]
        public string OrderID { get; set; }
        [JsonProperty("account")]
        public string Account { get; set; }
        [JsonProperty("marketID")]
        public string MarketID { get; set; }
        [JsonProperty("instrumentID")]
        public string InstrumentID { get; set; }
        [JsonProperty("buySell")]
        public string BuySell { get; set; }
        [JsonProperty("requestID")]
        public string RequestID { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
