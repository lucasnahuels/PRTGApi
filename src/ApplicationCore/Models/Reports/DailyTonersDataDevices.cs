using System;

namespace ApplicationCore.Models.Reports
{
    public class DailyTonersDataDevices : BaseModel
    {
        public Device Device { get; set; }
        public DateTime Date { get; set; }
        public int BlackTonersUsed { get; set; }
        public int CyanTonersUsed { get; set; }
        public int MagentaTonersUsed { get; set; }
        public int YellowTonersUsed { get; set; }
    }
}
