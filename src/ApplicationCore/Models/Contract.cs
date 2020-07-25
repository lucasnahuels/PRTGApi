using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Amazon.Extensions.CognitoAuthentication;

namespace ApplicationCore.Models
{
    public class Contract : BaseModel
    {
        public Company Company { get; set; }
        public Device Printer { get; set; }
        public int BlackAndWhiteSheets { get; set; }
        public int ColorSheets { get; set; }
        public int Month { get; set; }    
        public float BlackAndWhitePrice { get; set; }
        public float ColorPrice { get; set; }
        public float SurplusBlackAndWhitePrice { get; set; }
        public float SurplusColorPrice { get; set; }
        [NotMapped]
        public IEnumerable<CognitoUser> Users { get; set; }
        public IEnumerable<User> UsersId { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
