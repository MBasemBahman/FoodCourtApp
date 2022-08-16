namespace Entities.DBModels.AppModels
{
    public class AppAttachment : AttachmentEntity
    {
        [DisplayName(nameof(Order))]
        [DefaultValue(1)]
        public int Order { get; set; }
    }
}
