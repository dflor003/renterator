using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Renterator.Services.Validators;

namespace Renterator.Services.Dto
{
    public class UserAccountCreationInfo
    {
        [Required]
        [ValidateEmail]
        public string Email { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be 8 - 30 characters long")]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
