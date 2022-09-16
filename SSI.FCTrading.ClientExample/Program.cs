using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using SSI.FCTrading.Client;
using SSI.FCTrading.Client.Models;
using SSI.FCTrading.Client.Models.Request;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SSI.FCTrading.ClientExample
{
    class Program
    {
        private static string _pathDataFile = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName("data.json"));
        private static string _contentDataFile = String.Empty;
        private static readonly string _url = ConfigurationManager.AppSettings["URL"];
        private static readonly string _code = ConfigurationManager.AppSettings["Code"];
        private static readonly string _consumerID = ConfigurationManager.AppSettings["ConsumerID"];
        private static readonly string _consumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
        private static readonly string _privateKey = ConfigurationManager.AppSettings["PrivateKey"];
        private static readonly string _isSave = ConfigurationManager.AppSettings["IsSave"];

        static void Main(string[] args)
        {
            Enum.TryParse(ConfigurationManager.AppSettings["TwoFactorType"], out TwoFactorType twoFactorType);
            var logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

            var AuthenProvider = new AuthenProvider(_url, _consumerID, _consumerSecret, _code,
                                                    _privateKey , bool.Parse(_isSave), twoFactorType,logger);

            var hubClient = new HubClientExample(AuthenProvider, logger);
            var apiClient = new ApiClient(_url, AuthenProvider, logger);
            hubClient.Start();
            bool isExit = false;
            while (!isExit)
            {
                logger.Information("Choice:");
                logger.Information("1. Get OTP");
                logger.Information("2. New Order");
                logger.Information("3. Cancel Order");
                logger.Information("4. Modify Order");
                logger.Information("5. Get Cash Account Balance");
                logger.Information("6. Get Derivative account balance");
                logger.Information("7. Get purchasing power margin of account");
                logger.Information("8. Get stock position.");
                logger.Information("9. Get derivative position.");
                logger.Information("10. Get max buy quantity (buy power).");
                logger.Information("11. Get max sell quantity (sell power).");
                logger.Information("12. Get account order history");
                logger.Information("13. Exit");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        GetOTP(apiClient, logger);
                        break;
                    case "2":
                        NewOrder(apiClient, logger);
                        break;
                    case "3":
                        CancelOrder(apiClient, logger);
                        break;
                    case "4":
                        ModifyOrder(apiClient, logger);
                        break;
                    case "5":
                        GetCashAccountBalance(apiClient, logger);
                        break;
                    case "6":
                        GetDerivativeAccountBalance(apiClient, logger);
                        break;
                    case "7":
                        GetPpmmrAccount(apiClient, logger);
                        break;
                    case "8":
                        GetStockPosition(apiClient, logger);
                        break;
                    case "9":
                        GetDerivativePosition(apiClient, logger);
                        break;
                    case "10":
                        GetMaxBuyQuantity(apiClient, logger);
                        break;
                    case "11":
                        GetMaxSellQuantity(apiClient, logger);
                        break;
                    case "12":
                        GetAccountOrderHistory(apiClient, logger);
                        break;
                    case "13":
                        isExit = true;
                        break;
                    default:
                        logger.Information("Please try again");
                        continue;
                }
            }
            logger.Information("Done");

        }  

        #region Support
        static void NewOrder(ApiClient client, ILogger logger)
        {
            var request = Wapper<NewOrderRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.NewOrder(request)));
        }
        static void GetCashAccountBalance(ApiClient client, ILogger logger)
        {
            var request = Wapper<CashAccountBalanceRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetCashAccountBalance(request)));
        }

        static void GetOTP(ApiClient client, ILogger logger)
        {
            logger.Information(JsonConvert.SerializeObject(client.GetOTP(new Client.Models.Request.OTPRequest()
            {
                ConsumerID = _consumerID,
                ConsumerSecret = _consumerSecret,
            })));
        }

        static void CancelOrder(ApiClient client, ILogger logger)
        {
            var request = Wapper<CancelOrderRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.CancelOrder(request)));
        }

        static void ModifyOrder(ApiClient client, ILogger logger)
        {
            var request = Wapper<ModifyOrderRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.ModifyOrder(request)));
        }

        static void GetDerivativeAccountBalance(ApiClient client, ILogger logger)
        {
            var request = Wapper<DerivativeAccountRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetDerivativeAccountBalance(request)));
        }

        static void GetPpmmrAccount(ApiClient client, ILogger logger)
        {
            var request = Wapper<PpmmrAccountRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetPpmmrAccount(request)));
        }

        static void GetStockPosition(ApiClient client, ILogger logger)
        {
            var request = Wapper<StockPositionRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetStockPosition(request)));
        }

        static void GetDerivativePosition(ApiClient client, ILogger logger)
        {
            var request = Wapper<DerivativePositionRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetDerivativePosition(request)));
        }


        static void GetMaxBuyQuantity(ApiClient client, ILogger logger)
        {
            var request = Wapper<MaxBuyQuantityAccountRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetMaxBuyQuantity(request)));
        }

        static void GetMaxSellQuantity(ApiClient client, ILogger logger)
        {
            var request = Wapper<MaxSellQuantityRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetMaxSellQuantity(request)));
        }

        static void GetAccountOrderHistory(ApiClient client, ILogger logger)
        {
            var request = Wapper<OrderHistoryAccountRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetAccountOrderHistory(request)));
        }

        static T Wapper<T>(ApiClient client, ILogger logger)
        {
            var name = typeof(T).Name;
            _contentDataFile = File.ReadAllText(_pathDataFile);
            var rawObj = JObject.Parse(_contentDataFile)[name].ToString();
            var result = JsonConvert.DeserializeObject<T>(rawObj);
            return result;
        }
        #endregion

    }

    class HubClientExample
    {
        private readonly AuthenProvider AuthenProvider;
        HubClient HubClient;
        private readonly ILogger _logger;
        public HubClientExample(AuthenProvider authenProvider, ILogger logger)
        {
            AuthenProvider = authenProvider;
            HubClient = new HubClient(ConfigurationManager.AppSettings["StreamURL"], AuthenProvider, logger);
            HubClient.SetNotifyId(ConfigurationManager.AppSettings["NotifyID"]);
            HubClient.CreateHandleCallBack(OnData);
            HubClient.CreateHandleErrorCallback(onErrro);
            _logger = logger;
        }
        public void Start()
        {
            HubClient.Start().Wait();
            _logger.Information("HubClient Started");
        }
        public void onErrro(string error)
        {
            _logger.Error(error);
        }
        public void Stop()
        {
            HubClient.Stop();
        }

        public void OnData(string data)
        {
            _logger.Information(data);
        }
    }
}
