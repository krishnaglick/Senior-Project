
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SrProj.Models.Context;
using SrProj.Utility.Attribute;

namespace SrProj.Models
{
    public class Role
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }

    public enum RoleID
    {
        [EnumDecorators.Name("Admin")]
        [EnumDecorators.Description("This role applies to all admin users in the system")]
        Admin = 0,
        [EnumDecorators.Name("Volunteer")]
        [EnumDecorators.Description("This role applies to all users in the system")]
        Volunteer = 1
    }
}