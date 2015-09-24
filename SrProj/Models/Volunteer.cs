
using System;

namespace SrProj.Models
{
    public class Volunteer : ModelBase
    {
        public Guid ID { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string passwordHash { get; set; }
        public string username { get; set; }
    }
}