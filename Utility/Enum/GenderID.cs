
using Utility.Attribute;

namespace Utility.Enum
{
    public enum GenderID
    {
        [EnumDecorators.Name("Male")]
        [EnumDecorators.Description("Male")]
        Male = 1,
        [EnumDecorators.Name("Female")]
        [EnumDecorators.Description("Female")]
        Female = 2,
        [EnumDecorators.Name("Transgender")]
        [EnumDecorators.Description("Transgender")]
        Transgender = 3
    }
}
