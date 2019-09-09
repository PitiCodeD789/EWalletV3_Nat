using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EWalletV2.Api.ViewModels.Pin
{
    public class UpdatePinCommand
    {
        [Required]
        [RegularExpression(@"^(\d{6})$")]
        public string NewPin { get; set; }
        [Required]
        [RegularExpression(@"^(\d{6})$")]
        public string OldPin { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
