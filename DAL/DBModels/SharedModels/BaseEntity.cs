namespace DAL.DBModels.SharedModels
{
    public class BaseEntity
    {
        [Key]
        [DisplayName(nameof(Id))]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName(nameof(CreatedAt))]
        public DateTime CreatedAt { get; set; }
    }
}
