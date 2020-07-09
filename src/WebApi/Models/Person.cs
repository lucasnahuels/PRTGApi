using Amazon.DynamoDBv2.DataModel;

namespace WebApi.Models
{
    public class Person : BaseModel
    {
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public string Email { get; set; }
    }
}
