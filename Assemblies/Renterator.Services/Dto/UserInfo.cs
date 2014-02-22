using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Renterator.Services.Validators;

namespace Renterator.Services.Dto
{
    public class UserInfo
    {
        public int Id { get; set; }

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

        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }

        public DateTime LastLoginDate { get; set; }

        public ICollection<WebsiteInfo> Websites { get; set; }
    }
}
