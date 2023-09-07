using GpsdClient.Models.GpsdModels;

namespace GpsdClient.Models.Events;

public class GpsWatchEventArgs
{
    public GpsWatchEventArgs(GpsdWatch gpsdWatch)
    {
        GpsdWatch = gpsdWatch;
    }

    public GpsdWatch GpsdWatch { get; init; }
}