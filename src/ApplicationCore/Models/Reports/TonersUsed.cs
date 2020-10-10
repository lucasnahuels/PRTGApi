using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models.Reports
{
    public class TonersUsed : BaseModel
    {
        public long DeviceId { get; set; }
        public DateTime DateAndTime { get; set; }
        public int Black { get; set; }
        public int Cyan { get; set; }
        public int Yellow { get; set; }
        public int Magenta { get; set; }
    }
}
