using System.Collections.Generic;

namespace WebApi.Models
{
    public class Printer
    {
        public string Device { get; set; }
    }

    public class DevicesSensor
    {
        public List<Printer> Devices{ get; set; }
    }
}
