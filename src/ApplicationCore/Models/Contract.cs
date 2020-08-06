using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class Contract : BaseModel
    {
        public Owner Owner { get; set; }
        public long OwnerId { get; set; }
        public int BlackAndWhiteLimitSet { get; set; }
        public int ColorLimitSet { get; set; }
        public int Month { get; set; }    
        public float BlackAndWhitePrice { get; set; }
        public float ColorPrice { get; set; }
        public float SurplusBlackAndWhitePrice { get; set; }
        public float SurplusColorPrice { get; set; }
        public ICollection<ContractDevice> ContractDevices { get; set; }
        public ICollection<ContractUser> ContractUsers { get; set; }
        public ICollection<ContractEmployee> ContractEmployees { get; set; }
    }
}
