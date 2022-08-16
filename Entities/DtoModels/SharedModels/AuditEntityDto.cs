
namespace Entities.DtoModels.SharedModels
{
    public class AuditEntityDto : BaseEntityDto
    {
        [DisplayName(nameof(CreatedBy))]
        public string CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime LastModifiedAtVal { get; set; }

        [DisplayName(nameof(LastModifiedAt))]
        public string LastModifiedAt => LastModifiedAtVal.AddHours(2).ToShortDateTimeString();

        [DisplayName(nameof(LastModifiedBy))]
        public string LastModifiedBy { get; set; }
    }
}
