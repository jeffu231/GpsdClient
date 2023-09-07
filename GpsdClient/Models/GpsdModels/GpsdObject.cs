using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using GpsdClient.Exceptions;

namespace GpsdClient.Models.GpsdModels
{
    [DataContract]
    public class GpsdObject: IGpsdObject
    {
        [DataMember(Name = "class")]
        public string Class { get; set; }

        public Type GetClassType()
        {
            TypeDictionary.TryGetValue(Class, out var result);

            if (result == null)
            {
                throw new UnknownTypeException(Class.ToString());
            }

            return result;
        }

        private static readonly Dictionary<string, Type> TypeDictionary = new Dictionary<string, Type>
        {
            {"VERSION", typeof(GpsdVersion)},
            {"DEVICES", typeof(GpsDevices)},
            {"WATCH", typeof(GpsdWatch)},
            {"TPV", typeof(GpsdTpv)},
            {"SKY", typeof(GpsSky)},
            {"PPS", typeof(GpsdPps)}
            
        };
    }
}
