
using System.ComponentModel.DataAnnotations.Schema;

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
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}