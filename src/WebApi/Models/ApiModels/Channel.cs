using System.Collections.Generic;

namespace WebApi.Models
{
    public class ChannelData
    {
        public string Name { get; set; }
        public ChannelInfo Info { get; set; }
        //public string Info_Raw { get; set; }
        public string Minimum { get; set; }
        public string Maximum { get; set; }
        //public string Condition { get; set; }
        public string LastValue { get; set; }
    }

    public class ChannelInfo
    {
        public List<ChannelInfoData> Data { get; set; }
    }

    public class ChannelInfoData
    {
        //public int Status { get; set; }
        //public int Id { get; set; }
        //public string Name { get; set; }
        public bool IsPercent { get; set; }
        //public string LastValue { get; set; }
        //public string AllTimeMinimum { get; set; }
        //public string AllTimeMaximum { get; set; }
        public string Average { get; set; }
    }
}
