using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Attribute;

namespace Utility.Enum
{
    public enum GenderID
    {
        [EnumDecorators.Name("Male")]
        [EnumDecorators.Description("Male")]
        Male = 0,
        [EnumDecorators.Name("Female")]
        [EnumDecorators.Description("Female")]
        Female = 1,
        [EnumDecorators.Name("Transgender")]
        [EnumDecorators.Description("Transgender")]
        Transgender = 2
    }
}
