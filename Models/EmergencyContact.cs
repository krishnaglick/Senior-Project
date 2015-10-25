using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class EmergencyContact : ModelBase, IAddress
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get { return string.Format("{0} {1}", this.FirstName, this.LastName); }
        }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    [ComplexType]
    public class PhoneNumber
    {
        [MinLength(10)]
        [MaxLength(10)]
        public string ContactNumber { get; set; }
    }
}