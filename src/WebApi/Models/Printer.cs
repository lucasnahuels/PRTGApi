using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
