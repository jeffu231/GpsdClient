using System.Threading;

namespace GpsdClient.Client;

public interface IGpsdClient
{
    //public event EventHandler<IGpsdObject> MessageReceived;
    
    bool Connect(CancellationToken token);

    bool Disconnect();
}