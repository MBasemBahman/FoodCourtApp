namespace Entities.DBModels.AppModels
{
    public class AppAttachment : AttachmentEntity
    {
        [ForeignKey(nameof(Branch))]
        [DisplayName(nameof(Branch))]
        public int? Fk_Branch { get; set; }

        [DisplayName(nameof(Branch))]
        public Branch Branch { get; set; }

        [DisplayName(nameof(Order))]
        [DefaultValue(1)]
        public int Order { get; set; }
    }
}
