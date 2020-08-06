namespace ApplicationCore.Models
{
    public class ContractEmployee
    {
        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public long ContractId { get; set; }
        public Contract Contract { get; set; }
    }
}
