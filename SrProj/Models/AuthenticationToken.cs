
using System;
using System.ComponentModel.DataAnnotations;

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