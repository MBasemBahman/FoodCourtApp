namespace DAL.DBModels.AuthModels
{
    [Index(nameof(UserName), IsUnique = true)]
    public class SystemUser : AuditEntity
    {
        [DisplayName(nameof(Name))]
        [Required(ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        [DisplayName(nameof(UserName))]
        [Required(ErrorMessage = "{0} is required")]
        public string UserName { get; set; }

        [DisplayName(nameof(Password))]
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
