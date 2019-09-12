using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Transaction
{
    public class SlipViewModel
    {
        public string OtherName { get; set; }
        public string OtherAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public string Reference { get; set; }
        public EW_Enumerations.EW_TypeTransectionEnum Type { get; set; }
    }
}
