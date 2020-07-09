using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class Company : BaseModel
    {
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public IEnumerable<Employee> Employees { get; set; }
    }
}
