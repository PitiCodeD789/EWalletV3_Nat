using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EWalletV2.Api.ViewModels.Auth
{
    public class GetTokenByRefreshTokenCommand
    {
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
