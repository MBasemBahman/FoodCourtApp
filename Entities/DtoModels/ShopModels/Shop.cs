using Entities.DBModels.ShopModels;
using Entities.Features.RequestFeatures;

namespace Entities.DtoModels.ShopModels
{
    public class ShopParameters : RequestParameters
    {
        [DisplayName(nameof(Fk_Branch))]
        public int Fk_Branch { get; set; }
    }

    public class ShopDto : AuditImageEntityDto
    {
        [DisplayName(nameof(Name))]
        public string Name { get; set; }

        [DisplayName(nameof(Order))]
        public int Order { get; set; }

        [DisplayName(nameof(Address))]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [DisplayName(nameof(GalleryCount))]
        public int GalleryCount { get; set; }
    }

    public class ShopCreateDto
    {
        [DisplayName(nameof(Fk_Branch))]
        public int Fk_Branch { get; set; }

        [DisplayName(nameof(ShopImageFile))]
        public IFormFile ShopImageFile { get; set; }

        [DisplayName(nameof(Name))]
        public string Name { get; set; }

        [DisplayName(nameof(Address))]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
    }

    public class ShopEditDto
    {
        [DisplayName(nameof(Fk_Branch))]
        public int Fk_Branch { get; set; }

        [DisplayName(nameof(ShopImageFile))]
        public IFormFile ShopImageFile { get; set; }

        [DisplayName(nameof(Name))]
        public string Name { get; set; }

        [DisplayName(nameof(Address))]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
    }
}
