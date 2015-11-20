
using Utility.Attribute;

namespace Utility.Enum
{
    public enum RaceID
    {
        [EnumDecorators.Name("White")]
        [EnumDecorators.Description("White")]
        White = 0,
        [EnumDecorators.Name("Black")]
        [EnumDecorators.Description("African American")]
        Black = 1,
        [EnumDecorators.Name("Hawaiian")]
        [EnumDecorators.Description("Hawaiian")]
        Hawaiian = 2,
        [EnumDecorators.Name("Native American")]
        [EnumDecorators.Description("Native American")]
        NativeAmerican = 3,
        [EnumDecorators.Name("Alaskan Native")]
        [EnumDecorators.Description("Alaskan Native")]
        Alaskan = 4,
        [EnumDecorators.Name("Pacific Islander")]
        [EnumDecorators.Description("Pacific Islander")]
        PacificIslander,
        [EnumDecorators.Name("Asian")]
        [EnumDecorators.Description("Asian")]
        Asian
    }
}
