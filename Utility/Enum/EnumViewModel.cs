
using System.Collections.Generic;
using Utility.Attribute;
using Utility.ExtensionMethod;

namespace Utility.Enum
{
    public class EnumViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public static EnumViewModel ParseEnum(object e)
        {
            return new EnumViewModel
            {
                id = (int)e,
                name = e.GetEnumAttribute<EnumDecorators.Name>().name,
                description = e.GetEnumAttribute<EnumDecorators.Description>().desc
            };
        }
    }
}
