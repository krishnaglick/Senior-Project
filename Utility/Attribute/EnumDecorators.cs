
namespace Utility.Attribute
{
    public class EnumDecorators
    {
        public class Description : System.Attribute
        {
            public string desc { get; set; }
            public Description(string desc)
            {
                this.desc = desc;
            }
        }

        public class Name : System.Attribute
        {
            public string name { get; set; }
            public Name(string name)
            {
                this.name = name;
            }
        }
    }
}