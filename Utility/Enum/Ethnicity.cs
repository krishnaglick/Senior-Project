﻿
using Utility.Attribute;

namespace Utility.Enum
{
    enum Ethnicity
    {
        [EnumDecorators.Name("Hispanic")]
        [EnumDecorators.Description("For Hispanic Patrons")]
        Hispanic = 0,
        [EnumDecorators.Name("Non-Hispanic")]
        [EnumDecorators.Description("For Non-Hispanic Patrons")]
        NonHispanic = 1
    }
}
