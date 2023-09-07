using GpsdClient.Models.GpsdModels;

namespace GpsdClient.Constants
{
    public static class GpsdConstants
    {
        public static GpsdWatch DefaultGpsdWatch = new GpsdWatch {Enable = true, Json = true};
        public const string DisableCommand = "?WATCH={\"enable\":false}";
        public const string PollCommand = "?POLL;";
    }
}
