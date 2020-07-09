namespace WebApi.Models
{
    public class Channel
    {
        public string Name { get; set; }
        public string Name_Raw { get; set; }
        public ChannelInfo Info { get; set; }
        public string Info_Raw { get; set; }
        public string Minimum { get; set; }
        public string Minimum_Raw { get; set; }
        public string Maximum { get; set; }
        public string Maximum_Raw { get; set; }
        public string Condition { get; set; }
        public int Condition_Raw { get; set; }
        public string LastValue { get; set; }
        public string LastValue_Raw { get; set; }
    }
}
