namespace Entities.DBModels.AuthModels
{
    [Index(nameof(UserName), IsUnique = true)]
    public class SystemUser : AuditEntity
    {
        [DisplayName(nameof(Name))]
        [Required(ErrorMessage = PropertyAttributeConstants.RequiredMsg)]
        public string Name { get; set; }

        [DisplayName(nameof(UserName))]
        [Required(ErrorMessage = PropertyAttributeConstants.RequiredMsg)]
        public string UserName { get; set; }

        [DisplayName(nameof(Password))]
        [Required(ErrorMessage = PropertyAttributeConstants.RequiredMsg)]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
