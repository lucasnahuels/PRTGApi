using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Models
{
    [DynamoDBTable("Contracts")]
    public class Contract : BaseModel
    {
        [DynamoDBProperty]
        public Company Company { get; set; }
        [DynamoDBProperty]
        public long PrinterId { get; set; }
        [DynamoDBProperty]
        public int BlackAndWhiteSheets { get; set; }
        [DynamoDBProperty]
        public int ColorSheets { get; set; }
        [DynamoDBProperty]
        public int Month { get; set; }    
        [DynamoDBProperty]
        public float BlackAndWhitePrice { get; set; }
        [DynamoDBProperty]
        public float ColorPrice { get; set; }
        [DynamoDBProperty]
        public float ExcedenteBlackAndWhitePrice { get; set; }
        [DynamoDBProperty]
        public float ExcedenteColorPrice { get; set; }
        //[DynamoDBProperty]
        //public IEnumerable<Users> Users { get; set; }
        [DynamoDBProperty]
        public IEnumerable<Employee> Employees { get; set; }
    }
}
