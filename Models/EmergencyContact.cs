
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{

    public class EmergencyContact : ModelBase
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName => $"{this.FirstName} {this.LastName}";
        [Required][MinLength(10)][MaxLength(10)]
        public string PhoneNumber { get; set; }
    }
}
