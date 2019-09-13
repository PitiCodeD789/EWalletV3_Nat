using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.DtoModels.Transaction
{
    public class TransactionDto : BaseTransaction
    {
        public string AccountNo { get; set; }
    }
}
