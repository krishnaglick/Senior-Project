
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

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }

    [ComplexType]
    public class PhoneNumber
    {
        [MinLength(10)]
        [MaxLength(10)]
        public string ContactNumber { get; set; }
    }
}
