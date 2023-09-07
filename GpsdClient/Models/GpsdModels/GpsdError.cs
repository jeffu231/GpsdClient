using System.Runtime.Serialization;

namespace GpsdClient.Models.GpsdModels;

[DataContract]
public class GpsdError: GpsdObject
{
    [DataMember(Name = "message")]
    public string Message { get; set; }

    public override string ToString()
    {
        return $"Error: {Message}";
    }
}