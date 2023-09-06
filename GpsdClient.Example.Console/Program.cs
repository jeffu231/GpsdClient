using GpsdClient.Models.ConnectionInfo;
using GpsdClient.Models.Events;

namespace GpsdClient.Example.Console
{
    public class Program
    {
        private delegate bool EventHandler();
        private static EventHandler _eventHandler;

        private static GpsService _gpsService;

        static void Main(string[] args)
        {
            _eventHandler += ExitHandler;

            var info = new GpsdInfo()
            {
                Address = "127.0.0.1",
                ReadFrequency = 5000
                //Port = 2947
            };
            _gpsService = new GpsService(info);

            _gpsService.RegisterTpvDataEvent(GpsdServiceOnLocationChanged);
            _gpsService.Connect();

            System.Console.WriteLine("Press enter to continue...");
            System.Console.ReadKey();
        }

        private static void GpsdServiceOnLocationChanged(object sender, GpsTpvEventArgs e)
        {
            System.Console.WriteLine(e.GpsdTpv.ToString());
        }

        private static bool ExitHandler()
        {
            return _gpsService == null || _gpsService.Disconnect();
        }
    }
}
