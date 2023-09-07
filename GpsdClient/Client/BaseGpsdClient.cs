using System;
using System.Threading;
using GpsdClient.Enums;
using GpsdClient.Models.ConnectionInfo;
using GpsdClient.Models.Events;

namespace GpsdClient.Client
{
    public abstract class BaseGpsdClient: IGpsdClient
    {
        #region Properties
        
        public bool IsRunning { get; set; }

        protected BaseGpsInfo GpsInfo { get; set; }

        #endregion
        
        #region Event handlers

        public event EventHandler<GpsTpvEventArgs> GpsTpvCallbackEvent;
        public event EventHandler<string> RawGpsCallbackEvent;
        public event EventHandler<GpsStatus> GpsStatusEvent;
        public event EventHandler<GpsErrorEventArgs> GpsErrorCallbackEvent;

        #endregion

        #region Constructors

        protected BaseGpsdClient(BaseGpsInfo gpsInfo)
        {
            GpsInfo = gpsInfo;
        }

        #endregion

        #region Connect and Disconnect

        public abstract bool Connect(CancellationToken token);

        public abstract bool Disconnect();

        #endregion

        #region Events Triggers
        
        protected virtual void OnGpsTpvReceived(GpsTpvEventArgs e)
        {
            GpsTpvCallbackEvent?.Invoke(this, e);
        }

        protected virtual void OnRawGpsDataReceived(string e)
        {
            RawGpsCallbackEvent?.Invoke(this, e);
        }

        protected virtual void OnGpsStatusChanged(GpsStatus e)
        {
            GpsStatusEvent?.Invoke(this, e);
        }

        protected virtual void OnGpsErrorReceived(GpsErrorEventArgs e)
        {
            GpsErrorCallbackEvent?.Invoke(this, e);
        }

        #endregion
    }
}