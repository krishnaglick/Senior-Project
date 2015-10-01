using System;
using System.ComponentModel;
using System.Data.SqlTypes;

namespace SrProj.Models
{
    public abstract class ModelBase
    {
        public Volunteer CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public Volunteer ModifyUser { get; set; }
        public DateTime? ModifyDate { get; set; }

        protected ModelBase()
        {
            CreateDate = DateTime.UtcNow;
        }
    }
}
