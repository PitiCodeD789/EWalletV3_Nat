using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Transaction
{
    public class GeneratePaymentViewModel
    {
        public string LastName { get; set; }
        public string AccountNumber { get; set; }
        public string CheckSum { get; set; }
    }
}
