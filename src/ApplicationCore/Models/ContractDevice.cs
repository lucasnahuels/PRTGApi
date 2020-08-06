namespace ApplicationCore.Models
{
    public class ContractDevice
    {
        public string ObjId { get; set; }
        public Device Device { get; set; }
        public long ContractId { get; set; }
        public Contract Contract { get; set; }
    }
}
