namespace ApplicationCore.Models
{
    public class ContractUser
    {
        public User User{ get; set; }
        public string UserId { get; set; }
        public long ContractId { get; set; }
        public Contract Contract { get; set; }
    }
}
