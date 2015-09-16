using SrProj.Utility.Attribute;

namespace SrProj.Utility.Enum
{
    public enum ServiceType
    {
        [EnumDecorators.Name("Visit")]
        [EnumDecorators.Description("This service represents when a patron visits for information.")]
        Visit = 0,
        [EnumDecorators.Name("Baby Luv")]
        [EnumDecorators.Description("This service is for patrons who come seeking pregnancy and child assistance.")]
        BabyLuv = 1,
        [EnumDecorators.Name("Lord's Pantry")]
        [EnumDecorators.Description("This service is for patrons who come for food.")]
        LordsPantry = 2,
        [EnumDecorators.Name("Medical")]
        [EnumDecorators.Description("This service is for patrons who need medical assistance.")]
        Medical = 3,
        [EnumDecorators.Name("Dental")]
        [EnumDecorators.Description("This service is for patrons who need dental assistance.")]
        Dental = 4
    }
}