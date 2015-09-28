using System;
using System.ComponentModel;
using System.Data.SqlTypes;

namespace SrProj.Models
{
    public abstract class ModelBase
    {
        //public ApplicationUser CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        //public ApplicationUser ModifyUser { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
