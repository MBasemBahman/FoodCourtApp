namespace Entities.DtoModels.AppModels
{
    public class AppAttachmentDto : AttachmentEntityDto
    {
        [DisplayName(nameof(Order))]
        [DefaultValue(1)]
        public int Order { get; set; }
    }

    public class AppAttachmentEditDto
    {
        [DisplayName(nameof(Order))]
        [DefaultValue(1)]
        public int Order { get; set; }
    }
}
