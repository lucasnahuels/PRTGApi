using System.Collections.Generic;

namespace ApplicationCore.Models
{
    public class Owner : BaseModel
    {
        public string Name { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
