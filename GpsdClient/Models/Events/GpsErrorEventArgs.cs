using System;

namespace GpsdClient.Models.Events;

public class GpsErrorEventArgs: EventArgs
{
    public GpsErrorEventArgs(string errorMessage)
    {
        ErrorMessageMessage = errorMessage;
    }

    public string ErrorMessageMessage { get; init; }
}