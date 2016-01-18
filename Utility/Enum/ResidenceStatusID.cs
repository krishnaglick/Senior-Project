
using Utility.Attribute;

namespace Utility.Enum
{
    public enum ResidenceStatusID
    {
        [EnumDecorators.Name("US Citizen")]
        [EnumDecorators.Description("Patron is a US Citizen")]
        Citizen = 0,
        [EnumDecorators.Name("Legal Resident")]
        [EnumDecorators.Description("Patron is a Legal Resident but not a US Citizen")]
        LegalResident = 1,
        [EnumDecorators.Name("Immigrant Refugee")]
        [EnumDecorators.Description("Patron is a Immigrant Refugee but not a US Citizen")]
        ImmigrantRefugee = 2
    }
}
