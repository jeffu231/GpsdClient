using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using GpsdClient.Models.ConnectionInfo;
using GpsdClient.Models.ConnectionInfo.Credentials;

namespace GpsdClient.Handlers
{
    public static class ProxyClientHandler
    {
        public static TcpClient GetTcpClient(GpsdInfo data)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = Uri.UriSchemeHttp,
                Host = data.ProxyAddress,
                Port = data.ProxyPort
            };

            var proxyUri = uriBuilder.Uri;
            
            var handler = new HttpClientHandler() { UseProxy = true };
            
            handler.Proxy = new WebProxy(proxyUri);
            
            if (data.ProxyCredentials != null)
            {
                var credentials = data.ProxyCredentials;
                if (credentials.GetType() == typeof(ProxyCredentials))
                {
                    handler.Proxy.Credentials = new NetworkCredential(credentials.ProxyUsername, ((ProxyCredentials)credentials).ProxyPassword);
                }
                else if (credentials.GetType() == typeof(SecureProxyCredentials))
                {
                    handler.Proxy.Credentials = new NetworkCredential(data.ProxyCredentials.ProxyUsername, ((SecureProxyCredentials)credentials).ProxyPassword);
                }
            }
            else
            {
                handler.UseDefaultCredentials = true;
            }
            
            var client = new HttpClient(handler);

            var responseStream = client.GetStreamAsync("http://" + data.Address + ":" + data.Port);
            responseStream.Wait();
            
            Debug.Assert(responseStream != null);

            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            var rsType = responseStream.GetType();
            var connectionProperty = rsType.GetProperty("Connection", flags);

            var connection = connectionProperty.GetValue(responseStream, null);
            var connectionType = connection.GetType();
            var networkStreamProperty = connectionType.GetProperty("NetworkStream", flags);

            var networkStream = networkStreamProperty.GetValue(connection, null);
            var nsType = networkStream.GetType();
            var socketProperty = nsType.GetProperty("Socket", flags);
            var socket = (Socket)socketProperty.GetValue(networkStream, null);

            return new TcpClient { Client = socket };
        }
    }
}
