using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models.Reports
{
    public class TonersUsed : BaseModel
    {
        public long DeviceId { get; set; }
        public DateTime DateAndTime { get; set; }
        public int BlackTonersUsed { get; set; }
        public int CyanTonersUsed { get; set; }
        public int YellowTonersUsed { get; set; }
        public int MagentaTonersUsed { get; set; }
    }
}
