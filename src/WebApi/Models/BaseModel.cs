using Amazon.DynamoDBv2.DataModel;

namespace WebApi.Models
{
    public class BaseModel
    {
        [DynamoDBHashKey]
        public long Id { get; set; }
    }
}
