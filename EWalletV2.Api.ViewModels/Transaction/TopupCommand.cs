using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Transaction
{
    public class TopupCommand
    {
        public string Email { get; set; }
        public string Amount { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
