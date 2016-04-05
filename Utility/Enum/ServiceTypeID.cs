using Utility.Attribute;

namespace Utility.Enum
{
    public enum ServiceTypeID
    {
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
        Dental = 4,
        [EnumDecorators.Name("Other")]
        [EnumDecorators.Description("This service represents when a patron visits for any other reason.")]
        Other = 5
    }
}