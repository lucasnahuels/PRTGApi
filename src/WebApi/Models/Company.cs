using System.Collections.Generic;

namespace WebApi.Models
{
    public class Company : BaseModel
    {
        public string Name { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
