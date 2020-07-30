namespace ApplicationCore.Models
{
    public class Employee : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Owner Owner { get; set; }
    }
}
