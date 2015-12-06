
using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace Models
{
    public interface ILogin
    {
        string Username { get; set; }
        string Password { get; set; }
    }

    public class Login : ILogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Volunteer : ModelBase, ILogin
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public string Username { get; set; }
        public virtual ICollection<RoleVolunteer> Roles { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [JsonIgnore][XmlIgnore]
        public string HashedPassword { get; set; }
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public static readonly PasswordHasher hasher = new PasswordHasher();

        public void SecurePassword()
        {
            this.HashedPassword = hasher.HashPassword(this.Password);
        }

        public PasswordVerificationResult VerifyPassword(string passwordToValidate)
        {
            return hasher.VerifyHashedPassword(this.HashedPassword, passwordToValidate);
        }
    }
}