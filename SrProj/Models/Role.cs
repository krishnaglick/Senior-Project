﻿
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SrProj.Models.Context;

namespace SrProj.Models
{
    public class Role
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        //TODO: There is likely a better place for this.
        [NotMapped]
        public static readonly string[] authRoles = new []
        {
            "Admin"
        };
    }
}