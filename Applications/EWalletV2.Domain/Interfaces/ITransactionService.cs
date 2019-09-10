using System;
using System.Collections.Generic;
using System.Text;
using EWalletV2.Api.ViewModels.Transaction;
using EWalletV2.Domain.DtoModels.Transaction;

namespace EWalletV2.Domain.Interfaces
{
    public interface ITransactionService
    {
        TopupDto Topup(string email, string referenceNumber);
        string GenerateTopUp(string account, decimal amount);
        PaymentDto Payment(string email, string merchantAccNo, decimal pay);
        //TransactionDto GetDetailTransaction(string email, int transactionId);
        List<TransactionDto> GetTransaction30Days(string email);
    }
}
