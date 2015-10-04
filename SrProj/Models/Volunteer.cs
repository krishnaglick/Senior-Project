
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using PasswordHasher = SrProj.Utility.Security.PasswordHasher;

namespace SrProj.Models
{
    public class Volunteer : ModelBase
    {
        [Key]
        [Required]
        [Index(IsUnique = true)]
        public string Username { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string HashedPassword { get; set; }
        [NotMapped]
        public string Password { get; set; }

        public void SecurePassword()
        {
            this.HashedPassword = PasswordHasher.EncryptPassword(this.Password);
        }

        public PasswordVerificationResult VerifyPassword(string passwordToValidate)
        {
            return PasswordHasher.VerifyPassword(this.HashedPassword, passwordToValidate);
        }
    }
}