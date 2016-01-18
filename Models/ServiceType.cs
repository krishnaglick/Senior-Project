
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ServiceType
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
    }
}