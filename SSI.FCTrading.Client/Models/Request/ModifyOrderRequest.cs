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
        [JsonProperty("orderID")]
        public string OrderID { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("quantity")]
        public long Quantity { get; set; }
        [JsonProperty("account")]
        public string Account { get; set; }
        [JsonProperty("instrumentID")]
        public string InstrumentID { get; set; }
        [JsonProperty("marketID")]
        public string MarketID { get; set; }
        [JsonProperty("buySell")]
        public string BuySell { get; set; }
        [JsonProperty("requestID")]
        public string RequestID { get; set; }
        [JsonProperty("orderType")]
        public string OrderType { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
