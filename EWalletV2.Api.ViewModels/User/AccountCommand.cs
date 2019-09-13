using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EWalletV2.Api.ViewModels.User
{
    public class AccountCommand
    {
        [RegularExpression(@"^(\d{10})$")]
        public string AccountNumber { get; set; }
    }
}
