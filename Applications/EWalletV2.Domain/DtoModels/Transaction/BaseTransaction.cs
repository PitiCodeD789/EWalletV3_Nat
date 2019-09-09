using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.DtoModels.Transaction
{
    public class BaseTransaction
    {
        public int TransactionId { get; set; }
        public string TransactionType { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}
