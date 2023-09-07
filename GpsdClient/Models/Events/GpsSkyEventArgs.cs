using System;
using GpsdClient.Enums;

namespace GpsdClient.Models.Events
{
    public class GpsSkyEventArgs : EventArgs
    {
        public GpsStatus Status { get; init; }
        
        public GpsSkyEventArgs(GpsStatus status)
        {
            Status = status;
        }
    }
}
