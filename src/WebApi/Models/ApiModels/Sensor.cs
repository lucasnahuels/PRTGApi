using System.Collections.Generic;

namespace WebApi.Models
{
    public class SensorDetails
    {
        public SensorDetailsData SensorData { get; set; }

    }
    public class SensorDetailsData
    {
        public string Name { get; set; }
        public string ParentDeviceName { get; set; }
        public string ParentDeviceId { get; set; }
    }
    public class SensorsData
    {
        public string SensorName { get; set; }
        public List<ChannelData> Channels { get; set; }
    }

    public class SensorList
    {
        public List<SensorApiModel> Channel { get; set; }
    }

    public class SensorApiModel
    {
        public string Group { get; set; }
        public string Device { get; set; }
        public int ObjId { get; set; }
    }

}
