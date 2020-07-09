using Amazon.DynamoDBv2.DataModel;

namespace WebApi.Models
{
    public class Employee : Person
    {
        [DynamoDBProperty]
        public Company Company { get; set; }
    }
}
