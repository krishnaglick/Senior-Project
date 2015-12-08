using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
            get { return string.Format("{0} {1}", this.FirstName, this.LastName); }
        }
        public short NumberInHousehold { get; set; }
        public bool Banned { get; set; }
        //Marital Status
        public MaritalStatus Marital { get; set; }
        //Residence Status
        public ResidenceStatus Residence { get; set; }
        //Gender
        public Gender Gender { get; set; }
        public Ethnicity Ethnicity { get; set; }
        //Race
        [Required]
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; }
        public virtual ICollection<ServiceEligibility> ServicesUsed { get; set; }
    }
}