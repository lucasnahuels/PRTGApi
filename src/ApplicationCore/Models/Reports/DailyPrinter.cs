using System;

namespace ApplicationCore.Models.Reports
{
    public class DailyPrinter : BaseModel
    {
        public long PrinterId { get; set; }
        public DateTime Date { get; set; }
        public int BlackAndWhiteCopies { get; set; }
        public int ColorCopies { get; set; }
    }
}
