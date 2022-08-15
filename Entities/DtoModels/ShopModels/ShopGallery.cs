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
    public class ShopGalleryCreateDto
    {
        [Required(ErrorMessage = PropertyAttributeConstants.RequiredMsg)]
        public IList<IFormFile> Files { get; set; }

        [DisplayName(nameof(Fk_Shop))]
        public int Fk_Shop { get; set; }
    }
}
