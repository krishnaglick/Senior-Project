
using Utility.Attribute;

namespace Utility.Enum
{
    public enum EthnicityID
    {
        [EnumDecorators.Name("Hispanic")]
        [EnumDecorators.Description("For Hispanic Patrons")]
        Hispanic = 0,
        [EnumDecorators.Name("Non-Hispanic")]
        [EnumDecorators.Description("For Non-Hispanic Patrons")]
        NonHispanic = 1
    }
}
