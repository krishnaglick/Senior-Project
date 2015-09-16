using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SrProj.Models
{
    public class Patron : ModelBase
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
        [Required]
        public DateTime DateOfBirth { get; set; }
        public IList<PhoneNumber> PhoneNumbers { get; set; }
        public virtual IList<Address> Addresses { get; set; }
        public virtual IList<EmergencyContact> EmergencyContacts { get; set; }
    }

    public class Address : ModelBase
    {
        [Key]
        public int ID { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    public class EmergencyContact : Address
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get { return string.Format("{0} {1}", this.FirstName, this.LastName); }
        }
        public IList<PhoneNumber> PhoneNumbers { get; set; }
    }

    [ComplexType]
    public class PhoneNumber
    {
        [MinLength(10)]
        [MaxLength(10)]
        public string ContactNumber { get; set; }
    }
}