using System;
using System.Collections.Generic;
using System.Text;
using EWalletV2.Domain.Entities;

namespace EWalletV2.Domain.Repoitories
{
    public interface ITransactionRepository
    {
        bool CheckReference(string referenceNumber);
        bool CreatNewTopUp(string referenceNumber, int otherId, string amount);
        bool CustomerTopup(int customerId, string referenceNumber);
        TransactionEntity GetTransactionByReferenceNumber(string referenceNumber);
        bool CreatNewPayment(int otherId, int customerId, decimal amount, string referenceNumber);
        List<TransactionEntity> GetTransactionByCustomerId(int customerId);
    }
}
