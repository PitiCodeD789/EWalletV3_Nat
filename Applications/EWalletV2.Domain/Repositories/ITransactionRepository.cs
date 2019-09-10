using System;
using System.Collections.Generic;
using System.Text;
using EWalletV2.Domain.Entities;

namespace EWalletV2.Domain.Repositories
{
    public interface ITransactionRepository
    {
        bool CheckReference(string referenceNumber);
        bool CreateNewTopUp(string referenceNumber, int otherId, decimal amount);
        List<TransactionEntity> Get30TransactionByCustomerId(int customerId);
        bool CreateNewPayment(int otherId, int customerId, decimal amount, string referenceNumber);
        TransactionEntity GetTransactionByReferenceNumber(string referenceNumber);
        bool CustomerTopup(int customerId, string referenceNumber);
        TransactionEntity GetTransactionByTransactionId(int transactionId);
    }
}
