using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class User
    {
        public string UserId { get; set; }
        public ICollection<ContractUser> ContractUsers { get; set; }
    }
}
