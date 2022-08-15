
namespace Entities.DtoModels.SharedModels
{
    public class AuditEntityDto : BaseEntityDto
    {
        public string CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime LastModifiedAtVal { get; set; }

        public string LastModifiedAt => LastModifiedAtVal.AddHours(2).ToShortDateTimeString();

        public string LastModifiedBy { get; set; }
    }
}
