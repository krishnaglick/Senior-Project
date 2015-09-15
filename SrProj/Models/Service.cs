using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SrProj.Models
{
    public class EnumDecorators
    {
        public class Description : Attribute
        {
            public string desc { get; set; }
            public Description(string desc)
            {
                this.desc = desc;
            }
        }

        public class Name : Attribute
        {
            public string name { get; set; }
            public Name(string name)
            {
                this.name = name;
            }
        }
    }
    public enum ServiceType
    {
        [EnumDecorators.Name("Visit")]
        [EnumDecorators.Description("This service represents when a patron visits for information.")]
        Visit = 0,
        BabyLuv = 1,
        LordsPantry = 2,
        Medical = 3,
        Dental = 4
    }
    public class Service
    {
    }
}