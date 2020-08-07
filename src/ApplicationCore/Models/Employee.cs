using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class Employee : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public long OwnerId { get; set; }
        public Owner Owner { get; set; }
        public ICollection<ContractEmployee> ContractEmployees { get; set; }
    }
}
