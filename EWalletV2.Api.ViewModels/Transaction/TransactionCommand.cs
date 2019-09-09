using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Transaction
{
    public class TransactionCommand
    {
        public string Email { get; set; }
        public int TransactionId { get; set; }
    }
}
