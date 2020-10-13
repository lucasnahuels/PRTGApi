namespace ApplicationCore.Models
{
    public class ContractUser
    {
        public User User{ get; set; }
        public string User_Id { get; set; }
        public long ContractId { get; set; }
        public Contract Contract { get; set; }
    }
}
