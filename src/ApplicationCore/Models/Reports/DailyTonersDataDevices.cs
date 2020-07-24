using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Models.Reports
{
    public class DailyTonersDataDevices
    {
        public Device Device { get; set; }
        public DateTime Date { get; set; }
        public int BlackTonersUsed { get; set; }
        public int CyanTonersUsed { get; set; }
        public int MagentaTonersUsed { get; set; }
        public int YellowTonersUsed { get; set; }
    }
}
