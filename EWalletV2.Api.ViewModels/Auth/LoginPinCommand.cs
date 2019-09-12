using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EWalletV2.Api.ViewModels.Auth
{
   public class LoginPinCommand
    {
        [Required]
        [RegularExpression(@"^(\d{6})$")]
        public string Pin { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}
