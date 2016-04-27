
using System.ComponentModel.DataAnnotations;
using Utility.Attribute;

namespace Models
{
    public class Address
    {
        [Key]
        public int ID { get; set; }
        [FilterableText]
        public string StreetAddress { get; set; }
        [FilterableText]
        public string City { get; set; }
        [FilterableText]
        public string State { get; set; }
        [FilterableText]
        public string Zip { get; set; }
    }
}
