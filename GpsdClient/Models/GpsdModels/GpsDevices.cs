using System.Collections.Generic;

namespace GpsdClient.Models.GpsdModels
{
    public class GpsDevices:GpsdObject
    {
        public List<GpsDevice> Devices { get; set; }
    }
}
