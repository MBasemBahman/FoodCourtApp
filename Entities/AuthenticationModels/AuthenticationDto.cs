using Entities.Features.ResponseFeatures;

namespace Entities.AuthenticationModels
{
    public class UploadFile
    {
        public IFormFile File { get; set; }
    }

    public class ChangePasswordDto
    {
        [Required(ErrorMessage = PropertyAttributeConstants.RequiredMsg)]
        [DataType(DataType.Password)]
        [DisplayName(nameof(OldPassword))]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = PropertyAttributeConstants.RequiredMsg)]
        [DataType(DataType.Password)]
        [DisplayName(nameof(NewPassword))]
        public string NewPassword { get; set; }
    }

    public class AuthenticationDto
    {
        [Required(ErrorMessage = PropertyAttributeConstants.RequiredMsg)]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [DisplayName(nameof(PhoneNumber))]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = PropertyAttributeConstants.RequiredMsg)]
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        [DisplayName(nameof(Password))]
        public string Password { get; set; }
    }

    public class TokenDto
    {
        [Required(ErrorMessage = PropertyAttributeConstants.RequiredMsg)]
        public string Token { get; set; }
    }

    public class AuthenticatedDto
    {
        [DisplayName(nameof(Id))]
        public int Id { get; set; }

        [DisplayName(nameof(Name))]
        public string Name { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public TokenResponse TokenResponse { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public TokenResponse RefreshTokenResponse { get; set; }
    }
}
