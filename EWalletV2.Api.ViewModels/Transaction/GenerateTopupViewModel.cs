using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Transaction
{
    public class GenerateTopupViewModel
    {
        public string ReferenceNumber { get; set; }
        public string FirstName { get; set; }
        public string CheckSum { get; set; }
        public double Amount { get; set; }
        public string AccountNumber { get; set; }
        public string ExpireDate { get; set; }
    }
}
