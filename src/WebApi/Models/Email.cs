using Amazon.DynamoDBv2.DataModel;

namespace WebApi.Models
{
    public class Email
    {
        [DynamoDBProperty]
        public int EmailId { get; set; }
        [DynamoDBProperty]
        public string EmailAdress { get; set; }
    }
}
