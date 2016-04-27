
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class PhoneNumber
    {
        [Key]
        public int ID { get; set; }
        public string phoneNumber { get; set; }
    }
}
