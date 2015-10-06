
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SrProj.Models
{
    public class Role
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public virtual ICollection<Volunteer> Volunteers { get; set; }
    }
}