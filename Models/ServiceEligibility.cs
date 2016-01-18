using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
