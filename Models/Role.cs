using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Utility.Enum;

namespace Models
{
    public class Role
    {
        [Key]
        public RoleID ID { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public virtual ICollection<Volunteer> Volunteers { get; set; }
    }
}