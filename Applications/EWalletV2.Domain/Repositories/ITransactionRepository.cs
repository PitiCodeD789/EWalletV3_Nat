using System;
using System.Collections.Generic;
using System.Text;
using EWalletV2.Domain.Entities;

namespace EWalletV2.Domain.Repositories
{
    public interface ITransactionRepository
    {
        bool CheckReference(string referenceNumber);
        bool CreatNewTopUp(string referenceNumber, int otherId, string amount);
        List<TransactionEntity> GetTransactionByCustomerId(int customerId);
        bool CreatNewPayment(int otherId, int customerId, decimal amount, string referenceNumber);
        TransactionEntity GetTransactionByReferenceNumber(string referenceNumber);
        bool CustomerTopup(int customerId, string referenceNumber);
        TransactionEntity GetTransactionByTransactionId(int transactionId);
    }
}
