using DAL.DBModels.ShopModels;

namespace DAL.DBModels.AppModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class Branch : AuditImageEntity
    {
        [DisplayName(nameof(Name))]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [DisplayName(nameof(Shops))]
        public ICollection<Shop> Shops { get; set; }
    }
}
