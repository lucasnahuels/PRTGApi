using System.Collections.Generic;

namespace PRTG_Api.Models
{
    public class Sensor
    {
        public string PrtgVersion { get; set; }
        public int TreeSize { get; set; }
        public List<Channel> Channels { get; set; }
    }
}
