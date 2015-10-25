
using Utility.Attribute;

namespace Utility.Enum
{
    public enum RoleID
    {
        [EnumDecorators.Name("Admin")]
        [EnumDecorators.Description("This role applies to all admin users in the system")]
        Admin = 1,
        [EnumDecorators.Name("Volunteer")]
        [EnumDecorators.Description("This role applies to all users in the system")]
        Volunteer = 2
    }
}