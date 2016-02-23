using Utility.Attribute;

namespace Utility.Enum
{
    public enum ServiceTypeID
    {
        [EnumDecorators.Name("Visit")]
        [EnumDecorators.Description("This service represents when a patron visits for information.")]
        Visit = 1,
        [EnumDecorators.Name("Baby Luv")]
        [EnumDecorators.Description("This service is for patrons who come seeking pregnancy and child assistance.")]
        BabyLuv = 2,
        [EnumDecorators.Name("Lord's Pantry")]
        [EnumDecorators.Description("This service is for patrons who come for food.")]
        LordsPantry = 3,
        [EnumDecorators.Name("Medical")]
        [EnumDecorators.Description("This service is for patrons who need medical assistance.")]
        Medical = 4,
        [EnumDecorators.Name("Dental")]
        [EnumDecorators.Description("This service is for patrons who need dental assistance.")]
        Dental = 5
    }
}