using System;
using GpsdClient.Models.GpsdModels;

namespace GpsdClient.Models.Events;

public class GpsTpvEventArgs: EventArgs
{
    public GpsTpvEventArgs(GpsdTpv gpsdTpv)
    {
        GpsdTpv = gpsdTpv;
    }
    
    public GpsdTpv GpsdTpv { get; init; }
}