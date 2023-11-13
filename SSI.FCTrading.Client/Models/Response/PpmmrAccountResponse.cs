using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Response
{
    public class PpmmrAccountResponse
    {
        public long collateralAsset { get; set; }
        public long callLMW { get; set; }
        public long liability { get; set; }
        public long eeOrigin { get; set; }
        public long forceLMV { get; set; }
        public long equity { get; set; }
        public long ee { get; set; }
        public long callMargin { get; set; }
        public long cashBalance { get; set; }
        public long purchasingPower { get; set; }
        public long callForcesell { get; set; }
        public long lmv { get; set; }
        public long marginCall { get; set; }
        public long withdrawal { get; set; }
        public long collateralA { get; set; }
        public string action { get; set; }
        public long marginRatio { get; set; }
        public long debt { get; set; }
        public long accruedlongerest { get; set; }
        public long holdRight { get; set; }
        public long preLoan { get; set; }
        public long fees { get; set; }
        public long buyUnmatch { get; set; }
        public long ap { get; set; }
        public long apT1 { get; set; }
        public long sellUnmatch { get; set; }
        public long cia { get; set; }
        public long ar { get; set; }
        public long arT1 { get; set; }
        public long ppCredit { get; set; }
        public long creditLimit { get; set; }
        public long totalAssets { get; set; }
        public long marginCallLMVSold { get; set; }
        public long lmvNonMarginable { get; set; }
        public long eeCredit { get; set; }
        public long totalEquity { get; set; }
        public long eE90 { get; set; }
        public long eE80 { get; set; }
        public long eE70 { get; set; }
        public long eE60 { get; set; }
        public long eE50 { get; set; }
    }

    //public class PpmmrAccountResponse
    //{
    //    public string message { get; set; }
    //    public long status { get; set; }
    //    public PpmmrAccountData data { get; set; }
    //}

}
