using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Visit : ModelBase
    {
        [Key]
        public int ID { get; set; }
        public Volunteer CreateVolunteer { get; set; }
        public Patron Patron { get; set; }
        public ServiceType Service { get; set; }
    }
}