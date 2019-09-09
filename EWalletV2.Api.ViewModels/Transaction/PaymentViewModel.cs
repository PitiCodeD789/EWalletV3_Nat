using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Transaction
{
    public class PaymentViewModel
    {
        public string Reference { get; set; }
        public DateTime CreateDatetime { get; set; }
    }
}
