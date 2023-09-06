﻿using System.Runtime.Serialization;

namespace GpsdClient.Models.GpsdModels
{
    [DataContract]
    public class GpsdVersion:GpsdObject
    {
        [DataMember(Name = "release")]
        public string Release { get; set; }

        [DataMember(Name = "rev")]
        public string Rev { get; set; }

        [DataMember(Name = "proto_major")]
        public int ProtoMajor { get; set; }

        [DataMember(Name = "proto_minor")]
        public int ProtoMinor { get; set; }

        public override string ToString()
        {
            return $"Release: {Release} - Revision: {Rev} - ProtoMajor: {ProtoMajor} - ProtoMinor: {ProtoMinor}";
        }
    }
}
