using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SrProj.Models
{
    public class AuthenticationToken
    {
        [Key]
        public Guid Token { get; set; }
        [Required]
        public DateTime LastAccessedTime { get; set; }
        [Required]
        public Volunteer AssociatedVolunteer { get; set; }
    }
}