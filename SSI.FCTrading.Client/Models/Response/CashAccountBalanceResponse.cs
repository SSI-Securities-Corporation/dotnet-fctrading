using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models
{
    public class CashAccountBalanceResponse
    {
        public string account { get; set; }
        public long cashBal { get; set; }
        public long cashOnHold { get; set; }
        public long secureAmount { get; set; }
        public long withdrawable { get; set; }
        public long receivingCashT1 { get; set; }
        public long receivingCashT2 { get; set; }
        public long matchedBuyVolume { get; set; }
        public long matchedSellVolume { get; set; }
        public long debt { get; set; }
        public long unMatchedBuyVolume { get; set; }
        public long unMatchedSellVolume { get; set; }
        public long paidCashT1 { get; set; }
        public long paidCashT2 { get; set; }
        public long cia { get; set; }
        public long purchasingPower { get; set; }
        public long totalAssets { get; set; }

    }
}
