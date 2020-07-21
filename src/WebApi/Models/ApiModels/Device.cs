using System.Collections.Generic;

namespace WebApi.Models
{
    public class DevicesSensor
    {
        public List<DeviceApiModel> Devices{ get; set; }
    }

    public class DeviceSensor
    {
        public List<DeviceApiModel> Device { get; set; }
    }

    public class DeviceApiModel
    {
        public string Device { get; set; }
        public int ObjId { get; set; }
        public List<SensorsData> SensorList { get; set; }
    }
}
