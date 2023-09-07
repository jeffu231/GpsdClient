using System;
using System.Threading;
using GpsdClient.Client;
using GpsdClient.Enums;
using GpsdClient.Models.ConnectionInfo;
using GpsdClient.Models.Events;

namespace GpsdClient
{
    public class GpsService
    {
        #region Private Properties

        private readonly BaseGpsdClient _client;

        #endregion

        #region Public Properties

        public bool IsRunning => _client.IsRunning;

        #endregion

        #region Constructors

        public GpsService(GpsdInfo gpsdInfo)
        {
            _client = new Client.GpsdClient(gpsdInfo);
        }

        #endregion

        #region Connect and Disconnect

        public bool Connect()
        {
            return _client.Connect(new CancellationToken());
        }

        public bool Disconnect()
        {
            return _client.Disconnect();
        }

        #endregion

        #region Register Events

        public void RegisterTpvDataEvent(Action<object, GpsTpvEventArgs> action)
        {
            _client.GpsTpvCallbackEvent += new EventHandler<GpsTpvEventArgs>(action);
        }

        public void RegisterRawDataEvent(Action<object, string> action)
        {
            _client.RawGpsCallbackEvent += new EventHandler<string>(action);
        }

        public void RegisterStatusEvent(Action<object, GpsStatus> action)
        {
            _client.GpsStatusEvent += new EventHandler<GpsStatus>(action);
        }

        public void RemoveTpvDataEvent(Action<object, GpsTpvEventArgs> action)
        {
            _client.GpsTpvCallbackEvent -= new EventHandler<GpsTpvEventArgs>(action);
        }

        public void RemoveRawDataEvent(Action<object, string> action)
        {
            _client.RawGpsCallbackEvent -= new EventHandler<string>(action);
        }

        public void RemoveStatusEvent(Action<object, GpsStatus> action)
        {
            _client.GpsStatusEvent -= new EventHandler<GpsStatus>(action);
        }
        
        #endregion
    }
}
