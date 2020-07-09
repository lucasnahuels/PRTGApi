namespace WebApi.Models
{
    public class ChannelData
    {
        public int Status { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPercent { get; set; }
        public string LastValue { get; set; }
        public string LastValueRaw { get; set; }
        public float AllTimeMinimum { get; set; }
        public float AllTimeMaximum { get; set; }
        public float AverageRaw { get; set; }
        public float Average { get; set; }
        public int LimitMode { get; set; }
        public int GraphMaxRaw { get; set; }
        public int GraphMinRaw { get; set; }
        public string GraphMax { get; set; }
        public string GraphMin { get; set; }
    }
}
