
using Utility.Attribute;

namespace Utility.Enum
{
    public enum EthnicityID
    {
        [EnumDecorators.Name("White")]
        [EnumDecorators.Description("White")]
        White = 1,
        [EnumDecorators.Name("Black")]
        [EnumDecorators.Description("African American")]
        Black = 2,
        [EnumDecorators.Name("Hawaiian")]
        [EnumDecorators.Description("Hawaiian")]
        Hawaiian = 3,
        [EnumDecorators.Name("Native American")]
        [EnumDecorators.Description("Native American")]
        NativeAmerican = 4,
        [EnumDecorators.Name("Alaskan Native")]
        [EnumDecorators.Description("Alaskan Native")]
        Alaskan = 5,
        [EnumDecorators.Name("Pacific Islander")]
        [EnumDecorators.Description("Pacific Islander")]
        PacificIslander = 6,
        [EnumDecorators.Name("Asian")]
        [EnumDecorators.Description("Asian")]
        Asian = 7
    }
}
