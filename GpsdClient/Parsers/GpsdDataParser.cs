using GpsdClient.Models.GpsdModels;
using Newtonsoft.Json;

namespace GpsdClient.Parsers
{
    public class GpsdDataParser
    {
        public IGpsdObject GetGpsData(string gpsData)
        {
            try
            {
                var classType = JsonConvert.DeserializeObject<GpsdObject>(gpsData);
                return (IGpsdObject)JsonConvert.DeserializeObject(gpsData, classType.GetClassType());
            }
            catch (JsonReaderException)
            {
                return null;
            }
            catch (JsonSerializationException)
            {
                return null;
            }
        }
    }
}
