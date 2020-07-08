using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRTG_Api.Models
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
