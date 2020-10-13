using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class User
    {
        public string User_Id { get; set; }
        public ICollection<ContractUser> ContractUsers { get; set; }
    }
}
