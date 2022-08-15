using DAL.DBModels.AppModels;

namespace DAL.DBModels.ShopModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class Shop : AuditImageEntity
    {
        [DisplayName(nameof(Name))]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

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
