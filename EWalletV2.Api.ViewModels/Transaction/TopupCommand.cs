using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EWalletV2.Api.ViewModels.Transaction
{
    public class TopupCommand
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression("^[0-9a-zA-Z]{18}$")]
        public string ReferenceNumber { get; set; }
    }
}
