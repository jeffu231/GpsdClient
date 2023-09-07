using GpsdClient.Constants;
using GpsdClient.Models.ConnectionInfo.Credentials;
using GpsdClient.Models.GpsdModels;

namespace GpsdClient.Models.ConnectionInfo
{
    public class GpsdInfo : BaseGpsInfo
    {
        public string Address { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 2947;

        public bool IsProxyEnabled { get; set; }

        public string ProxyAddress { get; set; }
        public int ProxyPort { get; set; }
        
        public BaseProxyCredentials ProxyCredentials { get; set; }

        public int RetryRead { get; set; } = 3;

        public GpsdWatch GpsWatch { get; set; } = GpsdConstants.DefaultGpsdWatch;

        public GpsdInfo()
        {
            ReadFrequency = 0;
        }

        public GpsdInfo(string address, int port = 2947) : this()
        {
            Address = address;
            Port = port;
        }
    }
}
