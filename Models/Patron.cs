using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Attribute;

namespace Models
{
    public class Patron : ModelBase
    {
        public override int GetHashCode()
        {
            return this.ID.GetHashCode() ^
                   this.FirstName.GetHashCode() ^
                   this.LastName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(Patron) &&
                (
                    ((Patron)obj).ID == this.ID ||
                    (
                        ((Patron)obj).FirstName == this.FirstName &&
                        ((Patron)obj).LastName == this.LastName   &&
                        ((Patron)obj).DateOfBirth == this.DateOfBirth
                    )
                );
        }

        [Key]
        public int ID { get; set; }
        [Required][FilterableText]
        public string FirstName { get; set; }
        [Required]
        [FilterableText]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get { return string.Format("{0} {1}", this.FirstName, this.LastName); }
        }
        [FilterableNumber]
        public short NumberInHousehold { get; set; }
        [FilterableBoolean]
        public bool Banned { get; set; }
        [FilterableDropdown]
        public MaritalStatus Marital { get; set; }
        [FilterableDropdown]
        public ResidenceStatus Residence { get; set; }
        [FilterableDropdown]
        public Gender Gender { get; set; }
        [FilterableDropdown]
        public Ethnicity Ethnicity { get; set; }
        [FilterableDropdown]
        public Race Race { get; set; }
        [Required][FilterableDate]
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; }
        public virtual ICollection<ServiceEligibility> ServicesUsed { get; set; }
    }
}