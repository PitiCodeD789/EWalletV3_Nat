using System;
using System.Collections.Generic;
using System.Text;
using EWalletV2.Domain.DtoModels.Transaction;

namespace EWalletV2.Domain.Interfaces
{
    public interface ITransactionService
    {
        PaymentDto Payment(string email, string merchantAccNo, decimal pay);
    }
}
