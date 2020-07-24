using System;

namespace ApplicationCore.Models.Reports
{
    public class DailyContadoresDataDevices : BaseModel
    {
        public Device Device { get; set; }
        public DateTime Date { get; set; }
        public int BlackAndWhiteCopies { get; set; }
        public int ColorCopies { get; set; }
    }
}
