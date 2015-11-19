using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ServiceEligibility : ModelBase
    {
        [Required]
        public ServiceType ServiceType { get; set; }
        [Required]
        public Patron Patron { get; set; }
    }
}
