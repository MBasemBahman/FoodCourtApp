namespace Entities.DtoModels.SharedModels
{
    public class ImageEntityDto : BaseEntityDto
    {
        [DisplayName(nameof(ImageUrl))]
        public string ImageUrl { get; set; }
    }

    public class AuditImageEntityDto : AuditEntityDto
    {
        [DisplayName(nameof(ImageUrl))]
        public string ImageUrl { get; set; }
    }
}
