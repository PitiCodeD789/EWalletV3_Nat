using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EWalletV2.Api.ViewModels.Transaction
{
    public class GenerateTopupCommand
    {
        [Required]
        public string Account { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
