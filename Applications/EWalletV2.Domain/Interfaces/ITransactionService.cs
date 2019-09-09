using System;
using System.Collections.Generic;
using System.Text;
using EWalletV2.Domain.DtoModels.Transaction;

namespace EWalletV2.Domain.Interfaces
{
    public interface ITransactionService
    {
        TopupDto Topup(string email, string referenceNumber);
        string GenerateTopUp(string email, string amount);
        PaymentDto Payment(string email, string merchantAccNo, decimal pay);
    }
}
