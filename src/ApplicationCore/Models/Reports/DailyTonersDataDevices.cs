using System;

namespace ApplicationCore.Models.Reports
{
    public class DailyTonersDataDevices : BaseModel
    {
        public long DeviceId { get; set; }
        public DateTime DateToday { get; set; }
        public int BlackTonersUsed { get; set; }
        public int CyanTonersUsed { get; set; }
        public int MagentaTonersUsed { get; set; }
        public int YellowTonersUsed { get; set; }
    }
}
