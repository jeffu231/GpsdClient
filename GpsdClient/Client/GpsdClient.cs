using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using GpsdClient.Constants;
using GpsdClient.Enums;
using GpsdClient.Exceptions;
using GpsdClient.Handlers;
using GpsdClient.Models.ConnectionInfo;
using GpsdClient.Models.Events;
using GpsdClient.Models.GpsdModels;
using GpsdClient.Parsers;

namespace GpsdClient.Client
{
    public class GpsdClient : BaseGpsdClient
    {
        #region Private Properties

        private TcpClient _client;
        private StreamReader _streamReader;
        private StreamWriter _streamWriter;

        private GpsdDataParser _gpsdDataParser;
        private int _retryReadCount;
        private DateTime? _previousReadTime;

        #endregion

        #region Constructors

        public GpsdClient(GpsdInfo connectionData) : base(connectionData)
        {
        }
        
        #endregion

        #region Connect and Disconnect

        public override bool Connect(CancellationToken token)
        {
            var data = (GpsdInfo)GpsInfo;

            IsRunning = true;
            OnGpsStatusChanged(GpsStatus.Connecting);

            try
            {
                _client = data.IsProxyEnabled ? ProxyClientHandler.GetTcpClient(data) : new TcpClient(data.Address, data.Port);
                _streamReader = new StreamReader(_client.GetStream());
                _streamWriter = new StreamWriter(_client.GetStream());

                _gpsdDataParser = new GpsdDataParser();

                var gpsData = "";
                while (string.IsNullOrEmpty(gpsData))
                {
                    gpsData = _streamReader.ReadLine();
                }

                var message = _gpsdDataParser.GetGpsData(gpsData);
                var version = message as GpsdVersion;
                if (version == null) return false;
                ExecuteGpsdCommand(data.GpsWatch.GetCommand());
                OnGpsStatusChanged(GpsStatus.Connected);


                StartGpsReading(data);
            }
            catch
            {
                Disconnect();
                throw;
            }
            return true;
        }

        public void StartGpsReading(GpsdInfo data)
        {
            if (_streamReader == null || !_client.Connected) throw new NotConnectedException();

            _retryReadCount = data.RetryRead;
            IsRunning = true;
            while (IsRunning)
            {
                if (!_client.Connected)
                {
                    throw new ConnectionLostException();
                }

                try
                {
                    var gpsData = _streamReader.ReadLine();
                    OnRawGpsDataReceived(gpsData);
                    if (gpsData == null)
                    {
                        if (_retryReadCount == 0)
                        {
                            Disconnect();
                            throw new ConnectionLostException();
                        }
                        _retryReadCount--;
                        continue;
                    }
                    
                    var message = _gpsdDataParser.GetGpsData(gpsData);
                    if (message == null)
                    {
                        Console.Out.WriteLine($"Null message for: {gpsData}");
                        continue;
                    }

                    if (message.Class == GpsdClassType.ERROR.ToString())
                    {
                        OnGpsErrorReceived(new GpsErrorEventArgs(message.ToString()));
                    }

                    if (message is GpsdTpv gpsLocation)
                    {
                        if (_previousReadTime != null && data.ReadFrequency != 0 &&
                            gpsLocation.Time.Subtract(TimeSpan.FromMilliseconds(data.ReadFrequency)) <= _previousReadTime)
                            continue;
                        _previousReadTime = gpsLocation.Time;
                        OnGpsTpvReceived(new GpsTpvEventArgs(gpsLocation));
                    }
                    
                }
                catch (IOException)
                {
                    Disconnect();
                    throw;
                }
            }
        }

        public override bool Disconnect()
        {
            if (!IsRunning) return true;
            IsRunning = false;
            ExecuteGpsdCommand(GpsdConstants.DisableCommand);

            _streamReader?.Close();
            _streamWriter?.Close();
            _client?.Close();

            OnGpsStatusChanged(GpsStatus.Disabled);

            return true;
        }

        #endregion

        #region GPSD Commands

        private void ExecuteGpsdCommand(string command)
        {
            Console.Out.WriteLine($"Sending command: {command}");
            if (_streamWriter == null) return;
            _streamWriter.WriteLine(command);
            _streamWriter.Flush();
        }

        #endregion
    }
}
