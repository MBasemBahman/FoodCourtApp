namespace Entities.DtoModels.SharedModels
{
    public class BaseEntityDto
    {
        public int Id { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public DateTime CreatedAtVal { get; set; }

        [DisplayName(nameof(CreatedAt))]
        public string CreatedAt => CreatedAtVal.AddHours(2).ToShortDateTimeString();
    }
}
