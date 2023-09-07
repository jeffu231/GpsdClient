using System;
using System.Runtime.Serialization;

namespace GpsdClient.Models.GpsdModels
{
    [DataContract]
    public class GpsdTpv: GpsdObject
    {
        /// <summary>
        /// Name of the originating device.
        /// </summary>
        [DataMember(Name = "device")]
        public string Device { get; set; } = string.Empty;

        /// <summary>
        /// NMEA mode:
        ///  0=unknown
        ///  1=no fix
        ///  2=2D
        ///  3=3D
        /// </summary>
        [DataMember(Name = "mode")]
        public int Mode { get; set; }
        
        /// <summary>
        /// GPS fix status:
        ///   0=Unknown,
        ///   1=Normal,
        ///   2=DGPS,
        ///   3=RTK Fixed,
        ///   4=RTK Floating,
        ///   5=DR,
        ///   6=GNSSDR,
        ///   7=Time (surveyed),
        ///   8=Simulated,
        ///   9=P(Y)
        /// </summary>
        [DataMember(Name = "status")]
        public int Status { get; set; }

        /// <summary>
        /// Time/date stamp in ISO8601 format, UTC.
        /// May have a fractional part of up to .001sec precision.
        /// May be absent if the mode is not 2D or 3D.
        /// May be present, but invalid, if there is no fix.
        /// Verify 3 consecutive 3D fixes before believing it is UTC.
        /// Even then it may be off by several seconds until the current leap seconds is known.
        /// </summary>
        [DataMember(Name = "time")]
        public DateTime Time { get; set; }

        /// <summary>
        /// Estimated time stamp error in seconds. Certainty unknown.
        /// </summary>
        [DataMember(Name = "ept")]
        public float Ept { get; set; }

        /// <summary>
        /// Latitude in degrees: +/- signifies North/South.
        /// </summary>
        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude in degrees: +/- signifies East/West.
        /// </summary>
        [DataMember(Name = "lon")]
        public double Longitude { get; set; }

        /// <summary>
        /// Course over ground, degrees from true north.
        /// </summary>
        [DataMember(Name = "track")]
        public float Track { get; set; }

        /// <summary>
        /// Speed over ground, meters per second.
        /// </summary>
        [DataMember(Name = "speed")]
        public float Speed { get; set; }
        
        /// <summary>
        /// Climb (positive) or sink (negative) rate, meters per second.
        /// </summary>
        [DataMember(Name = "climb")]
        public float Climb { get; set; }
        
        /// <summary>
        /// Deprecated. Undefined. Use altHAE or altMSL.
        /// </summary>
        [DataMember(Name = "alt")]
        public float Alt { get; set; }
        
        [DataMember(Name = "altHAE")]
        public float AltHAE { get; set; }

        /// <summary>
        /// Current datum. Hopefully WGS84.
        /// </summary>
        [DataMember(Name = "datum")] 
        public string Datum { get; set; } = string.Empty;

        /// <summary>
        /// Speed in miles per hour 
        /// </summary>
        public double Mph => Speed * 2.23694;

        /// <summary>
        /// Speed in Kilometers per hour
        /// </summary>
        public double Kph => Speed * 3.6000059687997;

        public override string ToString()
        {
            return $"Class: {Class} - Device: {Device} - Mode: {Mode} - Datum: {Datum} " +
                   $"- Time: {Time} - Ept: {Ept} " +
                   $"- Latitude: {Latitude} - Longitude: {Longitude} " +
                   $"- Track: {Track} - Alt: {Alt} - AltHAE: {AltHAE} - Climb: {Climb} " +
                   $"- Speed: {Speed} - Speed Mph: {Mph} - Speed Kph: {Kph}" +
                   $"- Status: {Status}";
        }
    }
}
