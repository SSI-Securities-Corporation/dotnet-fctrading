using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Response
{
    public class PpmmrAccountData
    {
        public int collateralAsset { get; set; }
        public int callLMW { get; set; }
        public int liability { get; set; }
        public int eeOrigin { get; set; }
        public int forceLMV { get; set; }
        public int equity { get; set; }
        public int ee { get; set; }
        public int callMargin { get; set; }
        public int cashBalance { get; set; }
        public int purchasingPower { get; set; }
        public int callForcesell { get; set; }
        public int lmv { get; set; }
        public int marginCall { get; set; }
        public int withdrawal { get; set; }
        public int collateralA { get; set; }
        public string action { get; set; }
        public int marginRatio { get; set; }
        public int debt { get; set; }
        public int accruedInterest { get; set; }
        public int holdRight { get; set; }
        public int preLoan { get; set; }
        public int fees { get; set; }
        public int buyUnmatch { get; set; }
        public int ap { get; set; }
        public int apT1 { get; set; }
        public int sellUnmatch { get; set; }
        public int cia { get; set; }
        public int ar { get; set; }
        public int arT1 { get; set; }
        public int ppCredit { get; set; }
        public int creditLimit { get; set; }
        public int totalAssets { get; set; }
        public int marginCallLMVSold { get; set; }
        public int lmvNonMarginable { get; set; }
        public int eeCredit { get; set; }
        public int totalEquity { get; set; }
        public int eE90 { get; set; }
        public int eE80 { get; set; }
        public int eE70 { get; set; }
        public int eE60 { get; set; }
        public int eE50 { get; set; }
    }

    public class PpmmrAccountResponse
    {
        public string message { get; set; }
        public int status { get; set; }
        public PpmmrAccountData data { get; set; }
    }

}
