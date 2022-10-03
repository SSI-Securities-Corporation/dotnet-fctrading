using Newtonsoft.Json;
using Serilog;
using SSI.FCTrading.Client.Models;
using SSI.FCTrading.Client.Models.Request;
using SSI.FCTrading.Client.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client
{
    public class ApiClient : IDisposable
    {
        private string _url;
        private ILogger _logger;
        private AuthenProvider _authenProvider;
        private readonly HttpClient _httpClient;
        public ApiClient(string url
            , AuthenProvider authenProvider
            , ILogger logger = null)
        {
            _url = url;
            _authenProvider = authenProvider;
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_url);
        }
        private TResponse MakePostRequest<TRequest, TResponse>(string path, TRequest request)
        {
            try
            {
                var data = JsonConvert.SerializeObject(request);
                var sign = _authenProvider.Sign(data);
                var postJsonItem = new StringContent(data, Encoding.UTF8, "application/json");

                var httpRq = new HttpRequestMessage
                {
                    RequestUri = new Uri(new Uri(_url), path),
                    Method = HttpMethod.Post,
                    Headers = {
                    { HttpRequestHeader.Authorization.ToString(), "Bearer " + _authenProvider.GetAccessToken().GetAwaiter().GetResult() },
                    { HttpRequestHeader.ContentType.ToString(), "application/json" },//use this content type if you want to send more than one content type
                    {"X-Signature", sign }
                },
                    Content = postJsonItem
                };
                var httpRs = _httpClient.SendAsync(httpRq).GetAwaiter().GetResult();
                httpRs.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<TResponse>(httpRs.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return default(TResponse);
            }
          
        }
        private TResponse MakeGetRequest<TRequest, TResponse>(string path, TRequest request)
        {
            try
            {
                var queryString = request.ToQueryString();
                var uri = path + "?" + queryString;
                var httpRq = new HttpRequestMessage
                {
                    RequestUri = new Uri(new Uri(_url), uri),
                    Method = HttpMethod.Get,
                    Headers = {
                    { HttpRequestHeader.Authorization.ToString(), "Bearer " + _authenProvider.GetAccessToken().GetAwaiter().GetResult() },
                },
                };
                var httpRs = _httpClient.SendAsync(httpRq).GetAwaiter().GetResult();
                httpRs.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<TResponse>(httpRs.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return default(TResponse);
            }
            
        }
        private async Task<TResponse> MakePostRequestAsync<TRequest, TResponse>(string path, TRequest request)
        {
            var data = JsonConvert.SerializeObject(request);
            var sign = _authenProvider.Sign(data);
            var postJsonItem = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var httpRq = new HttpRequestMessage
            {
                RequestUri = new Uri(new Uri(_url), path),
                Method = HttpMethod.Post,
                Headers = {
                    { HttpRequestHeader.Authorization.ToString(), "Bearer " +await _authenProvider.GetAccessToken() },
                    { HttpRequestHeader.ContentType.ToString(), "application/json" },//use this content type if you want to send more than one content type
                    {"X-Signature", sign }
                },
                Content = postJsonItem
            };
            var httpRs =await _httpClient.SendAsync(httpRq);
            httpRs.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<TResponse>(await httpRs.Content.ReadAsStringAsync());
        }
        private async Task<TResponse> MakeGetRequestAsync<TRequest, TResponse>(string path, TRequest request)
        {
            var queryString = request.ToQueryString();
            var uri = path + "?" + queryString;
            var httpRq = new HttpRequestMessage
            {
                RequestUri = new Uri(new Uri(_url), uri),
                Method = HttpMethod.Get,
                Headers = {
                    { HttpRequestHeader.Authorization.ToString(), "Bearer " + await _authenProvider.GetAccessToken() },
                },
            };
            var httpRs = await _httpClient.SendAsync(httpRq);
            httpRs.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<TResponse>( await httpRs.Content.ReadAsStringAsync());
        }

        public SingleResponse<NewOrderResponse> NewOrder(NewOrderRequest order)
        {
            return MakePostRequest<NewOrderRequest, SingleResponse<NewOrderResponse>>(UrlConfigs.NEW_ORDER, order);
        }
        public SingleResponse<CashAccountBalanceResponse> GetCashAccountBalance(CashAccountBalanceRequest req)
        {
            return MakeGetRequest<CashAccountBalanceRequest, SingleResponse<CashAccountBalanceResponse>>(UrlConfigs.GET_CASH_ACCOUNT_BALANCE, req);
        }
        public Task<SingleResponse<NewOrderResponse>> NewOrderAsync(NewOrderRequest order)
        {
            return MakePostRequestAsync<NewOrderRequest, SingleResponse<NewOrderResponse>>(UrlConfigs.NEW_ORDER, order);
        }


        public Task<SingleResponse<CashAccountBalanceResponse>> GetCashAccountBalanceAsync(CashAccountBalanceRequest req)
        {
            return MakeGetRequestAsync<CashAccountBalanceRequest, SingleResponse<CashAccountBalanceResponse>>(UrlConfigs.GET_CASH_ACCOUNT_BALANCE, req);
        }
     
        public SingleResponse<CancelOrderResponse> CancelOrder(CancelOrderRequest order)
        {
            return MakePostRequest<CancelOrderRequest, SingleResponse<CancelOrderResponse>>(UrlConfigs.CANCEL_ORDER, order);
        }

        public SingleResponse<OTPResponse> GetOTP(OTPRequest otpRequest)
        {
            return MakePostRequest<OTPRequest, SingleResponse<OTPResponse>>(UrlConfigs.GET_OTP, otpRequest);
        }

        public SingleResponse<ModifyOrderResponse> ModifyOrder(ModifyOrderRequest order)
        {
            return MakePostRequest<ModifyOrderRequest, SingleResponse<ModifyOrderResponse>>(UrlConfigs.MODIFY_ORDER, order);
        }

        public SingleResponse<DerivativeAccountResponse> GetDerivativeAccountBalance(DerivativeAccountRequest request)
        {
            return MakeGetRequest<DerivativeAccountRequest, SingleResponse<DerivativeAccountResponse>>(UrlConfigs.DERIV_ACCTBAL, request);
        }

        public SingleResponse<PpmmrAccountResponse> GetPpmmrAccount(PpmmrAccountRequest request)
        {
            return MakeGetRequest<PpmmrAccountRequest, SingleResponse<PpmmrAccountResponse>>(UrlConfigs.PPMMR_ACCOUNT, request);
        }
        public SingleResponse<StockPositionResponse> GetStockPosition(StockPositionRequest request)
        {
            return MakeGetRequest<StockPositionRequest, SingleResponse<StockPositionResponse>>(UrlConfigs.STOCK_POSITION, request);
        }

        public SingleResponse<DerivativePositionResponse> GetDerivativePosition(DerivativePositionRequest request)
        {
            return MakeGetRequest<DerivativePositionRequest, SingleResponse<DerivativePositionResponse>>(UrlConfigs.DERIV_POSITION, request);
        }

        public SingleResponse<MaxBuyQuantityAccountResponse> GetMaxBuyQuantity(MaxBuyQuantityAccountRequest request)
        {
            return MakeGetRequest<MaxBuyQuantityAccountRequest, SingleResponse<MaxBuyQuantityAccountResponse>>(UrlConfigs.MAX_BUY_QTY, request);
        }


        public SingleResponse<MaxSellQuantityResponse> GetMaxSellQuantity(MaxSellQuantityRequest request)
        {
            return MakeGetRequest<MaxSellQuantityRequest, SingleResponse<MaxSellQuantityResponse>>(UrlConfigs.MAX_SELL_QTY, request);
        }


        public SingleResponse<OrderHistoryAccountResponse> GetAccountOrderHistory(OrderHistoryAccountRequest order)
        {
            return MakeGetRequest<OrderHistoryAccountRequest, SingleResponse<OrderHistoryAccountResponse>>(UrlConfigs.ORDER_HISTORY, order);
        }


        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
