using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class Device
    {
        public string ObjId { get; set; }
        public ICollection<ContractDevice> ContractDevices { get; set; }
    }
}
