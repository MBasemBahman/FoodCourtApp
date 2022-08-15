namespace DAL.DBModels.SharedModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class LookUpEntity : BaseEntity
    {
        [DisplayName(nameof(Name))]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class AuditLookUpEntity : AuditEntity
    {
        [DisplayName(nameof(Name))]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }
    }
}
