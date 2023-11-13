using Microsoft.AspNet.SignalR.Client.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Core;
using SSI.FCTrading.Client;
using SSI.FCTrading.Client.Models;
using SSI.FCTrading.Client.Models.Request;
using SSI.FCTrading.ClientExample.Extensions;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

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


        private static async Task Main(string[] args)
        {
            var logger = MyLogger.CreateLogger();
            
            while (true)
            {
                Console.WriteLine("1. FCTrading API");
                Console.WriteLine("2. FCTrading Streaming");
                Console.WriteLine("3. Exit");
                var command = Console.ReadLine();
                switch (command)
                {

                    case "1":
                        FCTradingAPI(logger);
                        break;
                    case "2":
                        await GetStreamData(logger);
                        break;
                    case "3":
                        break;

                    default:
                        logger.Information("Please try again");
                        continue;
                }
            }
        }
        public static void FCTradingAPI(ILogger logger) 
        {
            bool isExit = false;
            while (!isExit)
            {
                Console.WriteLine("1. Get OTP");
                Console.WriteLine("2. Get and set Access token");
                Console.WriteLine("3. New Order");
                Console.WriteLine("4. Cancel Order");
                Console.WriteLine("5. Modify Order");
                Console.WriteLine("6. Der New Order");
                Console.WriteLine("7. Der Cancel Order");
                Console.WriteLine("8. Der Modify Order");
                Console.WriteLine("9. Get Cash Account Balance");
                Console.WriteLine("10. Get Derivative account balance");
                Console.WriteLine("11. Get purchasing power margin of account");
                Console.WriteLine("12. Get stock position.");
                Console.WriteLine("13. Get derivative position.");
                Console.WriteLine("14. Get max buy quantity (buy power).");
                Console.WriteLine("15. Get max sell quantity (sell power).");
                Console.WriteLine("16. Get account order history");
                Console.WriteLine("17. Get order book.");
                Console.WriteLine("18. Get audit order book");
                Console.WriteLine("19. Exit");
                var command = Console.ReadLine();
                switch (command)
                {

                    case "1":
                        GetOTP(logger);
                        break;
                    case "2":
                        SaveAccessToken(logger);
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
                        DerNewOrder(logger);
                        break;

                    case "7":
                        DerCancelOrder(logger);
                        break;

                    case "8":
                        DerModifyOrder(logger);
                        break;

                    case "9":
                        GetCashAccountBalance(logger);
                        break;

                    case "10":
                        GetDerivativeAccountBalance(logger);
                        break;

                    case "11":
                        GetPpmmrAccount(logger);
                        break;

                    case "12":
                        GetStockPosition(logger);
                        break;

                    case "13":
                        GetDerivativePosition(logger);
                        break;

                    case "14":
                        GetMaxBuyQuantity(logger);
                        break;

                    case "15":
                        GetMaxSellQuantity(logger);
                        break;

                    case "16":
                        GetAccountOrderHistory(logger);
                        break;

                    case "17":
                        GetOrderBook(logger);
                        break;

                    case "18":
                        GetAuditOrderBook(logger);
                        break;

                    case "19":
                        isExit = true;
                        break;

                    default:
                        logger.Information("Please try again");
                        continue;
                }
            }
            logger.Information("Done");
        }

        //public static void FCTradingStream(ILogger logger)
        //{
        //    bool isExit = false;
        //    while (!isExit)
        //    {
        //        Console.WriteLine("Choice:");
        //        Console.WriteLine("1. Get OTP");
        //        Console.WriteLine("2. Get and set Access token");
        //        Console.WriteLine("3. New Order");
        //        Console.WriteLine("5. Exit");
        //        var command = Console.ReadLine();
        //        switch (command)
        //        {

        //            case "1":
        //                GetOTP(logger);
        //                break;
        //            case "2":
        //                SaveAccessToken(logger);
        //                break;

        //            case "3":
        //                GetStreamData(logger);
        //                break;

        //            case "5":
        //                isExit = true;
        //                break;

        //            default:
        //                logger.Information("Please try again");
        //                continue;
        //        }
        //    }
        //    logger.Information("Done");
        //}

        #region Support

        private static void NewOrder(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            var request = Wapper<NewOrderRequest>(client, logger);
            request.requestID = new Random().Next(0, 9999999).ToString();
            request.deviceId = Extentions.GetMACAddress();
            if (configuration["IsSave"].Equals("false"))
            {
                Console.WriteLine("Code: ");
                var code = Console.ReadLine();
                request.code = code;
            }
            logger.Information(JsonConvert.SerializeObject(client.NewOrder(request)));
        }

        private static void DerNewOrder(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            var request = Wapper<DerNewOrderRequest>(client, logger);
            request.requestID = new Random().Next(0, 9999999).ToString();
            request.deviceId = Extentions.GetMACAddress();
            if (configuration["IsSave"].Equals("false"))
            {
                Console.WriteLine("Code: ");
                var code = Console.ReadLine();
                request.code = code;
            }
            logger.Information(JsonConvert.SerializeObject(client.DerNewOrder(request)));
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
            var url = configuration["URL"];
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
        private static void SaveAccessToken(ILogger logger)
        {
            try
            {
                Enum.TryParse(configuration["TwoFactorType"], out TwoFactorType twoFactorType);
                var code = configuration["Code"];
                if (twoFactorType == TwoFactorType.OTP && configuration["IsSave"].Equals("true"))
                {
                    Console.WriteLine("Code: ");
                    code = Console.ReadLine();
                }
                var authenProvider = new AuthenProvider(configuration["URL"], configuration["ConsumerID"], configuration["ConsumerSecret"], code,
                                                        configuration["PrivateKey"], bool.Parse(configuration["IsSave"]), twoFactorType, logger);
                var token = authenProvider.GetAccessToken(true).Result;
                logger.Information("Get Token Success:" + JsonConvert.SerializeObject(token));
            } catch (Exception ex)
            {
                logger.Error("Get Token Fail: " + ex.Message);
            }
            
        }
        

        private static void CancelOrder(ILogger logger)
        {
            try
            {
                var client = ReadParamConfig(logger);
                var request = Wapper<CancelOrderRequest>(client, logger);
                request.requestID = new Random().Next(0, 9999999).ToString();
                request.deviceId = Extentions.GetMACAddress();
                if (configuration["IsSave"].Equals("false"))
                {
                    Console.WriteLine("Code: ");
                    var code = Console.ReadLine();
                    request.code = code;
                }
                logger.Information(JsonConvert.SerializeObject(client.CancelOrder(request)));
            } catch (Exception ex)
            {
                logger.Error("CancelOrder Fail: " + ex.Message);
            }

}

        private static void ModifyOrder(ILogger logger)
        {
            try
            {
                var client = ReadParamConfig(logger);
                var request = Wapper<ModifyOrderRequest>(client, logger);
                request.requestID = new Random().Next(0, 9999999).ToString();
                request.deviceId = Extentions.GetMACAddress();
                if (configuration["IsSave"].Equals("false"))
                {
                    Console.WriteLine("Code: ");
                    var code = Console.ReadLine();
                    request.code = code;
                }
                logger.Information(JsonConvert.SerializeObject(client.ModifyOrder(request)));
            }
            catch (Exception ex)
            {
                logger.Error("ModifyOrder Fail: " + ex.Message);
            }
        }

        private static void DerCancelOrder(ILogger logger)
        {
            try
            {
                var client = ReadParamConfig(logger);
                var request = Wapper<DerCancelOrderRequest>(client, logger);
                request.requestID = new Random().Next(0, 9999999).ToString();
                request.deviceId = Extentions.GetMACAddress();
                if (configuration["IsSave"].Equals("false"))
                {
                    Console.WriteLine("Code: ");
                    var code = Console.ReadLine();
                    request.code = code;
                }
                logger.Information(JsonConvert.SerializeObject(client.DerCancelOrder(request)));
            }
            catch (Exception ex)
            {
                logger.Error("DerCancelOrder Fail: " + ex.Message);
            }
            
        }

        private static void DerModifyOrder(ILogger logger)
        {
            try
            {
                var client = ReadParamConfig(logger);
                var request = Wapper<DerModifyOrderRequest>(client, logger);
                request.requestID = new Random().Next(0, 9999999).ToString();
                request.deviceId = Extentions.GetMACAddress();
                if (configuration["IsSave"].Equals("false"))
                {
                    Console.WriteLine("Code: ");
                    var code = Console.ReadLine();
                    request.code = code;
                }
                logger.Information(JsonConvert.SerializeObject(client.DerModifyOrder(request)));
            }
            catch (Exception ex)
            {
                logger.Error("DerCancelOrder Fail: " + ex.Message);
            }
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

        private static void GetOrderBook(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            var request = Wapper<OrderBookRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetOrderBook(request)));
        }

        private static void GetAuditOrderBook(ILogger logger)
        {
            var client = ReadParamConfig(logger);
            var request = Wapper<AuditOrderBookRequest>(client, logger);
            logger.Information(JsonConvert.SerializeObject(client.GetAuditOrderBook(request)));
        }

        private static async Task GetStreamData(ILogger logger)
        {
            try
            {
                Enum.TryParse(configuration["TwoFactorType"], out TwoFactorType twoFactorType);
                var authenProvider = new AuthenProvider(configuration["URL"], configuration["ConsumerID"], configuration["ConsumerSecret"], configuration["Code"],
                                                        configuration["PrivateKey"], bool.Parse(configuration["IsSave"]), twoFactorType, logger);
                //Enum.TryParse(configuration["TwoFactorType"], out TwoFactorType twoFactorType);
                //var authenProvider = new AuthenProvider(configuration["URL"], configuration["ConsumerID"], configuration["ConsumerSecret"], code,
                //                                        configuration["PrivateKey"], bool.Parse(configuration["IsSave"]), twoFactorType, logger);
                var hubClient = new HubClientExample(authenProvider, configuration, logger);
                await hubClient.Start();
            } catch (Exception ex)
            {
                logger.Error("HubClient Start Err: "+ ex.Message);
            }
            
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

        public async Task Start()
        {
            await HubClient.Start();
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
            Console.WriteLine(JsonConvert.SerializeObject(data));
            _logger.Information(data);
        }
    }
}