using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class AuthenticationToken : ModelBase
    {
        [Key]
        public Guid Token { get; set; }
        [NotMapped]
        public DateTime LastAccessedTime
        {
            get
            {
                var lastAccessedTime = this.ModifyDate ?? this.CreateDate;
                this.ModifyDate = DateTime.UtcNow;
                return lastAccessedTime;
            }
        }

        [Required]
        public Volunteer AssociatedVolunteer { get; set; }
    }
}