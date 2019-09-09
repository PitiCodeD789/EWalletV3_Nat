using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EWalletV2.Api.ViewModels.Pin
{
   public class CheckForgotPinCommand
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }

    }
}
