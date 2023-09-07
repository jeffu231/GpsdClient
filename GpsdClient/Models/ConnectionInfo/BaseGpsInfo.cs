using GpsdClient.Enums;

namespace GpsdClient.Models.ConnectionInfo
{
    public abstract class BaseGpsInfo
    {
        public GpsCoordinateSystem CoordinateSystem { get; set; } = GpsCoordinateSystem.GeoEtrs89;

        public int ReadFrequency { get; set; }
    }
}
