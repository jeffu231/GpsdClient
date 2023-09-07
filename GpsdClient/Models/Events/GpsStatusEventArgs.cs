using GpsdClient.Enums;

namespace GpsdClient.Models.Events;

public class GpsStatusEventArgs
{
    public GpsStatus Status { get; set; }

    public GpsStatusEventArgs()
    {
            
    }

    public GpsStatusEventArgs(GpsStatus status)
    {
        Status = status;
    }
}