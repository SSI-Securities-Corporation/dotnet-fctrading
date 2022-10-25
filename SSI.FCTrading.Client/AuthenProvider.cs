using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using SSI.FCTrading.Client.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client
{
    public class AuthenProvider
    {
        string _url;
        string _id;
        string _secret;
        string _code;
        bool _saveCode;
        private readonly ILogger _logger;
        private static long _tokenTime = 0;
        private string _privateKey;
        private SecurityKey _securityKey;
        private TwoFactorType _twoFactorType;
        public AuthenProvider(string url, string consumerId, string consumerSecret, string code, string privateKey,
                              bool saveCode = true, TwoFactorType twoFactorType = TwoFactorType.PIN, ILogger logger = null)
        {
            _logger = logger;
            _url = url;
            _id = consumerId;
            _secret = consumerSecret;
            _code = code;
            _saveCode = saveCode;
            _twoFactorType = twoFactorType;
            _privateKey = privateKey;
            _securityKey = Extentions.CreateKey(_privateKey);
            // Change minimum of RS256 key size to 1024 (default is 2048)
            var mainclass = typeof(AsymmetricSignatureProvider)
                       .GetField(nameof(AsymmetricSignatureProvider.DefaultMinimumAsymmetricKeySizeInBitsForSigningMap), BindingFlags.Public | BindingFlags.Static);
            var field = mainclass.GetValue(null) as Dictionary<string, int>;
            if (field != null)
            {
                field["RS256"] = 1024;

            }

            var mainclass2 = typeof(AsymmetricSignatureProvider).GetField(nameof(AsymmetricSignatureProvider.DefaultMinimumAsymmetricKeySizeInBitsForVerifyingMap), BindingFlags.Public | BindingFlags.Static);
            var field2 = mainclass2.GetValue(null) as Dictionary<string, int>;
            if (field2 != null)
            {
                field2["RS256"] = 1024;
            }


        }
        public string Sign(string data)
        {

            try
            {
                var signatureProvider = _securityKey.CryptoProviderFactory.CreateForSigning(_securityKey, "RS256", true);
                if (signatureProvider == null)
                    throw new InvalidOperationException("signatureProvider is null when generate signature");
                byte[] sig = signatureProvider.Sign(Encoding.UTF8.GetBytes(data));

                return Extentions.ByteArrayToString(sig);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Signature need format with hexadecimal string");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> GetAccessToken()
        {
            try
            {
                var _accessToken = await TakeAccessToken();
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(_accessToken);
                var tokenS = jsonToken as JwtSecurityToken;
                var exp = tokenS.Claims.First(claim => claim.Type == "exp").Value;
                _tokenTime = long.Parse(exp);
                return _accessToken;
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed to get access token");
                throw ex;
            }

        }
        private async Task<string> TakeAccessToken()
        {
            using (var client = new HttpClient())
            {
                var accessTokenRequest = new AccessTokenRequest()
                {
                    ConsumerID = _id,
                    ConsumerSecret = _secret,
                    Code = _code,
                    IsSave = _saveCode,
                    TwoFactorType = _twoFactorType
                };
                var postJsonItem = new StringContent(JsonConvert.SerializeObject(accessTokenRequest), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_url + UrlConfigs.GET_ACCESS_TOKEN, postJsonItem);

                response.EnsureSuccessStatusCode();
                var data = JsonConvert.DeserializeObject<SingleResponse<AccessTokenResponse>>(await response.Content.ReadAsStringAsync());
                if (data.status == (int)HttpStatusCode.OK)
                {

                    return data.data.accessToken;
                }
                else
                    throw new HttpRequestException(data.message);
            }
        }
        private bool CheckTokenLifeTime()
        {
            try
            {
                var datetimeNow = (long)DateTime.Now.ToUniversalTime().Subtract(
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                ).TotalSeconds;
                var timeSpanNumber = _tokenTime - datetimeNow;
                if (timeSpanNumber <= 0) return false;

                return timeSpanNumber > 600; // Revoke access token before it expired in 10 minute
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, ex.Message);
                return false; // token exp invalid
            }
        }
    }
}
