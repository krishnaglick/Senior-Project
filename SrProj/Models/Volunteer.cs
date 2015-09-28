
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PasswordHasher = SrProj.Utility.Security.PasswordHasher;

namespace SrProj.Models
{
    public class Volunteer : ModelBase
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Index("IX_Volunteer_Username", IsUnique = true)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        private string HashedPassword;
        [NotMapped]
        public string Password {
            get { return this.HashedPassword; }
            set { HashedPassword = PasswordHasher.EncryptPassword(value); }
        }
    }
}