
using Utility.Attribute;

namespace Utility.Enum
{
    public enum ResidenceStatusID
    {
        [EnumDecorators.Name("US Citizen")]
        [EnumDecorators.Description("Patron is a US Citizen")]
        Citizen = 1,
        [EnumDecorators.Name("Legal Resident")]
        [EnumDecorators.Description("Patron is a Legal Resident but not a US aCitizen")]
        LegalResident = 2,
        [EnumDecorators.Name("Immigrant Refugee")]
        [EnumDecorators.Description("Patron is a Immigrant Refugee but not a US Citizen")]
        ImmigrantRefugee = 3
    }
}
