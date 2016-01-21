
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class ServiceEligibility : ModelBase
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public ServiceType ServiceType { get; set; }
        [Required]
        public Patron Patron { get; set; }
    }
}
