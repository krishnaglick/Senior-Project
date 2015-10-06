using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SrProj.Models
{
    public class Visit
    {
        public Visit()
        {
            this.CreateDate = DateTime.UtcNow;
        }
        [Key]
        public int ID { get; set; }
        public Volunteer CreateVolunteer { get; set; }
        public virtual ICollection<Patron> Patrons { get; set; }
        public ServiceType Service { get; set; }
        public DateTime CreateDate { get; set; }
    }
}