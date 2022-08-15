using Entities.Features.RequestFeatures;

namespace Entities.DtoModels.ShopModels
{
    public class ShopGalleryParameters : RequestParameters
    {
        [DisplayName(nameof(Fk_Shop))]
        public int Fk_Shop { get; set; }
    }

    public class ShopGalleryDto : ImageEntityDto
    {
    }
}
