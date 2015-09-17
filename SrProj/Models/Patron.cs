using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Cryptography.X509Certificates;

namespace SrProj.Models
{
    public class PatronContext : DbContext
    {
        public PatronContext() : base("potato") { }
        public DbSet<Patron> Patrons { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class Patron : ModelBase
    {
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

        public override int GetHashCode()
        {
            return this.ID.GetHashCode()        ^
                   this.FirstName.GetHashCode() ^
                   this.LastName.GetHashCode();
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
        [Required]
        public DateTime DateOfBirth { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public ICollection<EmergencyContact> EmergencyContacts { get; set; }
    }

    public interface IAddress
    {
        string StreetAddress { get; set; }
        string City { get; set; }
        string County { get; set; }
        string State { get; set; }
        string Zip { get; set; }
    }

    [ComplexType]
    public class Address : IAddress
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

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