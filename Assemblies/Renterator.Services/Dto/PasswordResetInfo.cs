using System;

namespace Renterator.Services.Dto
{
    public class PasswordResetInfo
    {
        public Guid Token { get; set; }

        public string Email { get; set; }
    }
}
