using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client.Models
{
    public enum TwoFactorType
    {
        PIN,
        OTP,
        CA
    }
    public class AccessTokenRequest
    {
        public string ConsumerID { get; set; }
        public string ConsumerSecret { get; set; }
        public TwoFactorType TwoFactorType { get; set; } = TwoFactorType.PIN;
        public string Code { get; set; }
        public bool IsSave { get; set; }
    }
}
