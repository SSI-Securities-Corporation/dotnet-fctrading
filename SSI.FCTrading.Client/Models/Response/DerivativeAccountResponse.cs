using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models.Response
{
    public class DerivativeAccountResponse
    {
        public string message { get; set; }
        public int status { get; set; }
        public DerivativeDataAccount data { get; set; }
    }

    public class DerivativeDataAccount
    {
        public string account { get; set; }
        public decimal accountBalance { get; set; }
        public decimal fee { get; set; }
        public decimal commission { get; set; }
        public decimal interest { get; set; }
        public decimal loan { get; set; }
        public decimal deliveryAmount { get; set; }
        public decimal floatingPL { get; set; }
        public decimal totalPL { get; set; }
        public decimal marginable { get; set; }
        public decimal depositable { get; set; }
        public decimal rcCall { get; set; }
        public decimal withdrawable { get; set; }
        public decimal nonCashDrawableRCCall { get; set; }
        public InternalAssets internalAssets { get; set; }
        public ExchangeAssets exchangeAssets { get; set; }
        public InternalMargin internalMargin { get; set; }
        public ExchangeMargin exchangeMargin { get; set; }
    }

    public class ExchangeAssets
    {
        public decimal cash { get; set; }
        public decimal validNonCash { get; set; }
        public decimal totalValue { get; set; }
        public decimal maxValidNonCash { get; set; }
        public decimal cashWithdrawable { get; set; }
        public decimal ee { get; set; }
    }

    public class ExchangeMargin
    {
        public decimal marginReq { get; set; }
        public decimal accountRatio { get; set; }
        public decimal usedLimitWarningLevel1 { get; set; }
        public decimal usedLimitWarningLevel2 { get; set; }
        public decimal usedLimitWarningLevel3 { get; set; }
        public decimal marginCall { get; set; }
    }

    public class InternalAssets
    {
        public decimal cash { get; set; }
        public decimal validNonCash { get; set; }
        public decimal totalValue { get; set; }
        public decimal maxValidNonCash { get; set; }
        public decimal cashWithdrawable { get; set; }
        public decimal ee { get; set; }
    }

    public class InternalMargin
    {
        public decimal initialMargin { get; set; }
        public decimal deliveryMargin { get; set; }
        public decimal marginReq { get; set; }
        public decimal accountRatio { get; set; }
        public decimal usedLimitWarningLevel1 { get; set; }
        public decimal usedLimitWarningLevel2 { get; set; }
        public decimal usedLimitWarningLevel3 { get; set; }
        public decimal marginCall { get; set; }
    }
}
