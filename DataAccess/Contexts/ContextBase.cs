﻿
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Models;

namespace DataAccess.Contexts
{
    public class ContextBase : DbContext
    {
        public ContextBase() : base(ConnectionString.ChosenConnection)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public ContextBase(string connectionString) : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    //TODO: We livin
    public class Database : ContextBase
    {
        public Database() { }
        public Database(string connectionString) : base(connectionString) { }

        public DbSet<Patron> Patrons { get; set; }
        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleVolunteer> RoleVolunteers { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<ResidenceStatus> ResidenceStatuses { get; set; }
        public DbSet<Ethnicity> Ethnicities { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

    }
}