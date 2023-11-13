using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using Microsoft.AspNet.SignalR.Client.Transports;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SSI.FCTrading.Client
{
    public delegate void OnReceived(string data);
    public class HubClient
    {
        private HubConnection _hubConnection;
        string _url;
        private readonly ILogger _logger;
        private readonly AuthenProvider _authenticationProvider;

        public const string HUB_NAME = "BroadcastHubV2";
        public delegate void DelegateRunner(string data);
        private IHubProxy _hubProxy;
        private event OnReceived _onBroadcast;
        private event OnReceived _onError;
        string _notifyId = "-1";
        private Timer ReconnectTimer;
        public HubClient(string url, AuthenProvider authenProvider, ILogger logger = null)
        {
            _url = url;
            _authenticationProvider = authenProvider;
            _logger = logger;
        }
        private void CreateHubClient(string accessToken, string notifyId, CancellationToken cancellationToken = default)
        {
            _logger?.Information("Create hub connection with accesssToken: {0}", accessToken);
            _hubConnection = new HubConnection(_url);
            _hubConnection.Headers.Add("Authorization", "Bearer " + accessToken);
            _hubConnection.Headers.Add("NotifyID", notifyId);
            _hubProxy = _hubConnection.CreateHubProxy(HUB_NAME);
            _hubProxy.On<string>("Broadcast", On_Broadcast);
            _hubProxy.On<string>("Error", On_Error);
            _hubConnection.StateChanged += On_StateChanged;
        }
        private void SchedulerReconnect(int seconds = 3)
        {
            ReconnectTimer = new Timer((state) =>
            {
                _logger?.Information("Reconect to TAPI stream!");
                Start().Wait();
            }, null, seconds * 1000, Timeout.Infinite);
        }
        public void On_StateChanged(StateChange stateChange)
        {
            try
            {
                _logger?.Information($"Order hub client change state from {stateChange.OldState} to {stateChange.NewState}");
                switch (stateChange.NewState)
                {
                    case ConnectionState.Connecting:
                        break;
                    case ConnectionState.Connected:
                        break;
                    case ConnectionState.Reconnecting:
                        _hubConnection.Headers.Remove("Authorization");
                        _hubConnection.Headers.Add("Authorization", "Bearer " + _authenticationProvider.GetAccessToken().GetAwaiter().GetResult());
                        break;
                    case ConnectionState.Disconnected:
                        SchedulerReconnect();// Reconnect if reconnect failed to disconnect
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed to proccess On_StateChanged event");
            }
        }
        public void On_Broadcast(string data)
        {
            try
            {
                _onBroadcast?.Invoke(data);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed when process broadcast event");
            }
        }
        public void On_Error(string msg)
        {
            try
            {
                _onError?.Invoke(msg);
            }
            catch (Exception ex)
            {
                _logger?.Error(ex, "Failed when process error");
            }
        }
        public void SetNotifyId(string Id)
        {
            _notifyId = Id;
        }
        public async Task Start(CancellationToken cancellationToken = default)
        {
            var accessToken = await _authenticationProvider.GetAccessToken(true);
            CreateHubClient(accessToken, _notifyId, cancellationToken);
            await _hubConnection.Start(new WebSocketTransport());
        }

        public void CreateHandleCallBack(OnReceived @onReceived)
        {
            _onBroadcast += @onReceived;
        }
        public void CreateHandleErrorCallback(OnReceived @onReceived)
        {
            _onError += @onReceived;
        }

        public void Stop()
        {
            if (_hubConnection == null) return;
            _hubConnection.Stop();
        }
    }
}
