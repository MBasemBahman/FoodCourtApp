using Entities.DBModels.ShopModels;

namespace Entities.DBModels.AppModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class Branch : AuditImageEntity
    {
        [DisplayName(nameof(Name))]
        [Required(ErrorMessage = PropertyAttributeConstants.RequiredMsg)]
        public string Name { get; set; }

        [DisplayName(nameof(Order))]
        [DefaultValue(1)]
        public int Order { get; set; }

        [DisplayName(nameof(Shops))]
        public ICollection<Shop> Shops { get; set; }

        [DisplayName(nameof(AppAttachments))]
        public ICollection<AppAttachment> AppAttachments { get; set; }
    }
}
