using Entities.DBModels.AppModels;

namespace Entities.DBModels.ShopModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class Shop : AuditImageEntity
    {
        [DisplayName(nameof(Name))]
        [Required(ErrorMessage = PropertyAttributeConstants.RequiredMsg)]
        public string Name { get; set; }

        [DisplayName(nameof(Order))]
        [DefaultValue(1)]
        public int Order { get; set; }

        [DisplayName(nameof(Address))]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [ForeignKey(nameof(Branch))]
        [DisplayName(nameof(Branch))]
        public int Fk_Branch { get; set; }

        [DisplayName(nameof(Branch))]
        public Branch Branch { get; set; }

        [DisplayName(nameof(ShopGallery))]
        public IList<ShopGallery> ShopGalleries { get; set; }
    }
}
