using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EWalletV2.Api.ViewModels.Transaction
{
    public class PaymentCommand
    {
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression(@"^(\d{10})$")]
        public string MerchantAccountNo { get; set; }
        [Required]
        public decimal Pay { get; set; }
    }
}
