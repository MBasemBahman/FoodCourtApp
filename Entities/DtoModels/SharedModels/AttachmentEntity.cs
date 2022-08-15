namespace Entities.DtoModels.SharedModels
{
    public class AttachmentEntityDto : BaseEntityDto
    {
        [DisplayName(nameof(FileName))]
        public string FileName { get; set; }

        [DisplayName(nameof(FileType))]
        public string FileType { get; set; }

        [DisplayName(nameof(FileLength))]
        public double FileLength { get; set; }

        [DisplayName(nameof(FileUrl))]
        public string FileUrl { get; set; }
    }

    public class AuditAttachmentEntityDto : AuditEntityDto
    {
        [DisplayName(nameof(FileName))]
        public string FileName { get; set; }

        [DisplayName(nameof(FileType))]
        public string FileType { get; set; }

        [DisplayName(nameof(FileLength))]
        public double FileLength { get; set; }

        [DisplayName(nameof(FileUrl))]
        public string FileUrl { get; set; }
    }
}
