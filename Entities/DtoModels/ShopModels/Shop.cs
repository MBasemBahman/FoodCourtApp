using Entities.DBModels.ShopModels;

namespace Entities.DtoModels.ShopModels
{
    public class ShopDto : AuditImageEntityDto
    {
        [DisplayName(nameof(Name))]
        public string Name { get; set; }

        [DisplayName(nameof(Address))]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [DisplayName(nameof(ShopGallery))]
        public IList<ShopGalleryDto> ShopGalleries { get; set; }
    }
}
