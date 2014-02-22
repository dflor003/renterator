using System.ComponentModel.DataAnnotations;

namespace Renterator.Services.Dto
{
    public class LoginInfo
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email required")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
        public string Password { get; set; }
    }
}
