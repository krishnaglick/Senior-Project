
using Utility.Attribute;

namespace Utility.Enum
{
    public enum MaritalStatusID
    {
        [EnumDecorators.Name("Single")]
        [EnumDecorators.Description("Patron has never been married")]
        Single = 1,
        [EnumDecorators.Name("Married")]
        [EnumDecorators.Description("Patron is married")]
        Married = 2,
        [EnumDecorators.Name("Divorced")]
        [EnumDecorators.Description("Patron is is divorced")]
        Divorced = 3,
        [EnumDecorators.Name("Legally Seperated")]
        [EnumDecorators.Description("Patron is legally seperated")]
        LegallySeperated = 4
    }
}
