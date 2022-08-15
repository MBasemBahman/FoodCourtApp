namespace DAL.DBModels.SharedModels
{
    public class ImageEntity : BaseEntity
    {
        [DisplayName(nameof(ImageUrl))]
        public string ImageUrl { get; set; }

        [DisplayName(nameof(StorageUrl))]
        [DataType(DataType.Url)]
        [Url]
        public string StorageUrl { get; set; }
    }

    public class AuditImageEntity : AuditEntity
    {
        [DisplayName(nameof(ImageUrl))]
        public string ImageUrl { get; set; }

        [DisplayName(nameof(StorageUrl))]
        [DataType(DataType.Url)]
        [Url]
        public string StorageUrl { get; set; }
    }
}
