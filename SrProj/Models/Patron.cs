using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SrProj.Models
{
    public class Patron : ModelBase
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
        [Key]
        new public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<PhoneNumber> PhoneNumbers { get; set; }
    }

    public class PhoneNumber : ModelBase
    {
        [Key]
        public int ID { get; set; }
        public string ContactNumber { get; set; }
    }
}