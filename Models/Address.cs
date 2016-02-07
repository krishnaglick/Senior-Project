
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Attribute;

namespace Models
{
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
        [FilterableText]
        public string StreetAddress { get; set; }
        [FilterableText]
        public string City { get; set; }
        [FilterableText]
        public string County { get; set; }
        [FilterableText]
        public string State { get; set; }
        [FilterableText]
        public string Zip { get; set; }
    }
}