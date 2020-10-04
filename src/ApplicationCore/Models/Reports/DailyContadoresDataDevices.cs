using System;

namespace ApplicationCore.Models.Reports
{
    public class DailyContadoresDataDevices : BaseModel
    {
        public long DeviceId { get; set; }
        public DateTime DateToday { get; set; }
        public int BlackAndWhiteCopies { get; set; }
        public int ColorCopies { get; set; }
    }
}
