namespace Dashboard.ViewModel
{
    public class LoginViewModel
    {
        [DisplayName("UserName")]
        [Required(ErrorMessage = "{0} is required")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string Password { get; set; }

        [DisplayName("Remember Me")]
        public bool Remember { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [DisplayName("Old Password")]
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string OldPassword { get; set; }

        [DisplayName("New Password")]
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string NewPassword { get; set; }
    }
}
