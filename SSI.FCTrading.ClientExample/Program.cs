using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using SSI.FCTrading.Client;
using SSI.FCTrading.Client.Models;
using SSI.FCTrading.Client.Models.Request;
using SSI.FCTrading.ClientExample.Extensions;
using System;
using System.IO;

namespace SSI.FCTrading.ClientExample
{
    internal class Program
    {
        private static string _pathDataFile = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileName("data.json"));
        private static string _contentDataFile = string.Empty;

        private static IConfiguration configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddNewtonsoftJsonFile("data.json", optional: true, reloadOnChange: true)
        .Build();

        private static void Main(string[] args)
        {
            var logger = MyLogger.CreateLogger();
            bool isExit = false;
            while (!isExit)
            {
                Console.WriteLine("Choice:");
                Console.WriteLine("1. Get OTP");
                Console.WriteLine("2. Stream FcTradding");
                Console.WriteLine("3. New Order");
                Console.WriteLine("4. Cancel Order");
                Console.WriteLine("5. Modify Order");
                Console.WriteLine("6. Get Cash Account Balance");
                Console.WriteLine("7. Get Derivative account balance");
                Console.WriteLine("8. Get purchasing power margin of account");
                Console.WriteLine("9. Get stock position.");
                Console.WriteLine("10. Get derivative position.");
                Console.WriteLine("11. Get max buy quantity (buy power).");
                Console.WriteLine("12. Get max sell quantity (sell power).");
                Console.WriteLine("13. Get account order history");
                Console.WriteLine("14. Exit");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        GetOTP(logger);
                        break;

                    case "2":
                        GetStreamData(logger);
                        break;

                    case "3":
                        NewOrder(logger);
                        break;

                    case "4":
                        CancelOrder(logger);
                        break;

                    case "5":
                        ModifyOrder(logger);
                        break;

                    case "6":
                        GetCashAccountBalance(logger);
                        break;

                    case "7":
                        GetDerivativeAccountBalance(logger);
                        break;

                    case "8":
                        GetPpmmrAccount(logger);
                        break;

                    case "9":
                        GetStockPosition(logger);
                        break;

                    case "10":
                        GetDerivativePosition(logger);
                        break;

                    case "11":
                        GetMaxBuyQuantity(logger);
                        break;

                    case "12":
                        GetMaxSellQuantity(logger);
                        break;

                    case "13":
                        GetAccountOrderHistory(logger);
                        break;

                    case "14":
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

        private static void NewOrder(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            var request = Wapper<NewOrderRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.NewOrder(request)));
        }

        private static void GetCashAccountBalance(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            var request = Wapper<CashAccountBalanceRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetCashAccountBalance(request)));
        }

        private static ApiClient ReadParamConfig(ILogger logger)
        {
            Enum.TryParse(configuration["TwoFactorType"], out TwoFactorType twoFactorType);
            var authenProvider = new AuthenProvider(configuration["URL"], configuration["ConsumerID"], configuration["ConsumerSecret"], configuration["Code"],
                                                    configuration["PrivateKey"], bool.Parse(configuration["IsSave"]), twoFactorType, logger);
            var apiClient = new ApiClient(configuration["URL"], authenProvider, logger);
            return apiClient;
        }

        private static void GetOTP(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            logger.Information(JsonConvert.SerializeObject(client.GetOTP(new Client.Models.Request.OTPRequest()
            {
                ConsumerID = configuration["ConsumerID"],
                ConsumerSecret = configuration["ConsumerSecret"],
            })));
        }

        private static void CancelOrder(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            var request = Wapper<CancelOrderRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.CancelOrder(request)));
        }

        private static void ModifyOrder(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            var request = Wapper<ModifyOrderRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.ModifyOrder(request)));
        }

        private static void GetDerivativeAccountBalance(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            var request = Wapper<DerivativeAccountRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetDerivativeAccountBalance(request)));
        }

        private static void GetPpmmrAccount(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            var request = Wapper<PpmmrAccountRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetPpmmrAccount(request)));
        }

        private static void GetStockPosition(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            var request = Wapper<StockPositionRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetStockPosition(request)));
        }

        private static void GetDerivativePosition(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            var request = Wapper<DerivativePositionRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetDerivativePosition(request)));
        }

        private static void GetMaxBuyQuantity(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            var request = Wapper<MaxBuyQuantityAccountRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetMaxBuyQuantity(request)));
        }

        private static void GetMaxSellQuantity(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            var request = Wapper<MaxSellQuantityRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetMaxSellQuantity(request)));
        }

        private static void GetAccountOrderHistory(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            var request = Wapper<OrderHistoryAccountRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetAccountOrderHistory(request)));
        }

        private static void GetStreamData(ILogger logger)
        {
            Enum.TryParse(configuration["TwoFactorType"], out TwoFactorType twoFactorType);
            var authenProvider = new AuthenProvider(configuration["URL"], configuration["ConsumerID"], configuration["ConsumerSecret"], configuration["Code"],
                                                    configuration["PrivateKey"], bool.Parse(configuration["IsSave"]), twoFactorType, logger);
            var hubClient = new HubClientExample(authenProvider, configuration, logger);
            hubClient.Start();
        }

        private static T Wapper<T>(ApiClient client, ILogger logger)
        {
            try
            {
                var name = typeof(T).Name;
                _contentDataFile = File.ReadAllText(_pathDataFile);
                var rawObj = JObject.Parse(_contentDataFile)[name].ToString();
                var result = JsonConvert.DeserializeObject<T>(rawObj);
                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
                return default(T);
            }
        }

        #endregion Support
    }

    internal class HubClientExample
    {
        private readonly AuthenProvider AuthenProvider;
        private HubClient HubClient;
        private readonly ILogger _logger;

        public HubClientExample(AuthenProvider authenProvider, IConfiguration configuration, ILogger logger)
        {
            AuthenProvider = authenProvider;
            HubClient = new HubClient(configuration["StreamURL"], AuthenProvider, logger);
            HubClient.SetNotifyId(configuration["NotifyID"]);
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