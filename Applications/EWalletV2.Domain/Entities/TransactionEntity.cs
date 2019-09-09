using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.Entities
{
    public class TransactionEntity : BaseEntity
    {
        public string TransactionType { get; set; }
        public int CustomerId { get; set; }
        public int OtherId { get; set; }
        public decimal Amount { get; set; }
    }
}
