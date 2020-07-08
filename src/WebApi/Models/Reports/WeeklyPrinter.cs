namespace WebApi.Models.Reports
{
    public class WeeklyPrinter : BaseModel
    {
        public long PrinterId { get; set; }
        public int Week { get; set; }
        public int Year { get; set; }
        public int BlackAndWhiteCopies { get; set; }
        public int ColorCopies { get; set; }
    }
}
