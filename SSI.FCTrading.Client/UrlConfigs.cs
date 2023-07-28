using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client
{
    public class UrlConfigs
    {
        public const string GET_OTP = "/api/v2/Trading/GetOTP";
        public const string GET_ACCESS_TOKEN = "/api/v2/Trading/AccessToken";       

        public const string NEW_ORDER = "/api/v2/Trading/NewOrder";
        public const string CANCEL_ORDER = "/api/v2/Trading/CancelOrder";
        public const string MODIFY_ORDER = "/api/v2/Trading/ModifyOrder";

        public const string GET_CASH_ACCOUNT_BALANCE = "/api/v2/Trading/cashAcctBal";
        public const string DERIV_ACCTBAL = "/api/v2/Trading/derivAcctBal";
        public const string PPMMR_ACCOUNT = "/api/v2/Trading/ppmmraccount";
        public const string STOCK_POSITION = "/api/v2/Trading/stockPosition";
        public const string DERIV_POSITION = "/api/v2/Trading/derivPosition";
        public const string MAX_BUY_QTY = "/api/v2/Trading/maxBuyQty";
        public const string MAX_SELL_QTY = "/api/v2/Trading/maxSellQty";
        public const string ORDER_HISTORY = "/api/v2/Trading/orderHistory";
    }
}
