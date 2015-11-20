using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Attribute;

namespace Utility.Enum
{
    public enum MaritalStatusID
    {
        [EnumDecorators.Name("Single")]
        [EnumDecorators.Description("Patron has never been married")]
        Single = 0,
        [EnumDecorators.Name("Married")]
        [EnumDecorators.Description("Patron is married")]
        Married = 1,
        [EnumDecorators.Name("Divorced")]
        [EnumDecorators.Description("Patron is is divorced")]
        Divorced = 2,
        [EnumDecorators.Name("Legally Seperated")]
        [EnumDecorators.Description("Patron is legally seperated")]
        LegallySeperated = 3
    }
}
