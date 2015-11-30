using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public virtual ICollection<Volunteer> Volunteers { get; set; }
    }
}