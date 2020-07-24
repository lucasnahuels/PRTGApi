using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models
{
    public class Device
    {
        [Key]
        public string ObjId { get; set; }
    }
}
