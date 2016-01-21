
using System;

namespace Utility.Attribute
{
    public enum FilterableAttributeType
    {
        Dropdown = 0,

        Text = 1,

        Number = 2,

        Boolean = 3,

        Date = 4
    }

    [AttributeUsage(AttributeTargets.Property)]
    public abstract class FilterableAttribute : System.Attribute
    {
        public FilterableAttributeType type { get; set; }
    }

    public class FilterableDropdown : FilterableAttribute
    {
        public FilterableDropdown()
        {
            this.type = FilterableAttributeType.Dropdown;
        }
    }

    public class FilterableText : FilterableAttribute
    {
        public FilterableText()
        {
            this.type = FilterableAttributeType.Text;
        }
    }

    public class FilterableNumber : FilterableAttribute
    {
        public FilterableNumber()
        {
            this.type = FilterableAttributeType.Number;
        }
    }

    public class FilterableBoolean : FilterableAttribute
    {
        public FilterableBoolean()
        {
            this.type = FilterableAttributeType.Boolean;
        }
    }

    public class FilterableDate : FilterableAttribute
    {
        public FilterableDate()
        {
            this.type = FilterableAttributeType.Date;
        }
    }
}
