namespace DAL.DBModels.SharedModels
{
    public class AttachmentEntity : BaseEntity
    {
        [DisplayName(nameof(FileName))]
        public string FileName { get; set; }

        [DisplayName(nameof(FileType))]
        public string FileType { get; set; }

        [DisplayName(nameof(FileLength))]
        public double FileLength { get; set; }

        [DisplayName(nameof(FileUrl))]
        public string FileUrl { get; set; }

        [DisplayName(nameof(StorageUrl))]
        [DataType(DataType.Url)]
        [Url]
        public string StorageUrl { get; set; }
    }

    public class AuditAttachmentEntity : AuditEntity
    {
        [DisplayName(nameof(FileName))]
        public string FileName { get; set; }

        [DisplayName(nameof(FileType))]
        public string FileType { get; set; }

        [DisplayName(nameof(FileLength))]
        public double FileLength { get; set; }

        [DisplayName(nameof(FileUrl))]
        public string FileUrl { get; set; }

        [DisplayName(nameof(StorageUrl))]
        [DataType(DataType.Url)]
        [Url]
        public string StorageUrl { get; set; }
    }
}
