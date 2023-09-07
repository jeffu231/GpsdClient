using System.Security;

namespace GpsdClient.Models.ConnectionInfo.Credentials
{
    public class SecureProxyCredentials : BaseProxyCredentials
    {
        public SecureString ProxyPassword { get; set; }

        public SecureProxyCredentials(string username, SecureString password) : base(username)
        {
            ProxyPassword = password;
        }
    }
}
