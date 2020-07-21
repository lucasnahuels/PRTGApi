using System.Collections.Generic;
using Amazon.Extensions.CognitoAuthentication;

namespace WebApi.Models
{
    public class Contract : BaseModel
    {
        public Company Company { get; set; }
        public long PrinterId { get; set; }
        public int BlackAndWhiteSheets { get; set; }
        public int ColorSheets { get; set; }
        public int Month { get; set; }    
        public float BlackAndWhitePrice { get; set; }
        public float ColorPrice { get; set; }
        public float ExcedenteBlackAndWhitePrice { get; set; }
        public float ExcedenteColorPrice { get; set; }
        public IEnumerable<CognitoUser> Users { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
