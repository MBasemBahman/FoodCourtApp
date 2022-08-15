namespace Entities.DBModels.ShopModels
{
    public class ShopGallery : ImageEntity
    {
        [ForeignKey(nameof(Shop))]
        [DisplayName(nameof(Shop))]
        public int Fk_Shop { get; set; }

        [DisplayName(nameof(Shop))]
        public Shop Shop { get; set; }

        [DisplayName(nameof(Order))]
        [DefaultValue(1)]
        public int Order { get; set; }
    }
}
