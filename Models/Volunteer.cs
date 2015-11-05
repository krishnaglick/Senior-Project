
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;

namespace Models
{
    public interface ILogin
    {
        string Username { get; set; }
        string Password { get; set; }
    }

    public class Volunteer : ModelBase, ILogin
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

        public object Add(string p)
        {
            throw new System.NotImplementedException();
        }
    }
}