using EWalletV2.Domain.DtoModels.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.Interfaces
{
    public interface ITransactionService
    {
        TopupDto Topup(string email, string referenceNumber);
        string GenerateTopUp(string email, string amount);
    }
}
