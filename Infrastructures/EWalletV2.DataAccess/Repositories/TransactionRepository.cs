using EWalletV2.Api.ViewModels;
using EWalletV2.DataAccess.Contexts;
using EWalletV2.Domain.Entities;
using EWalletV2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EWalletV2.DataAccess.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _context;
        public TransactionRepository(DataContext context)
        {
            _context = context;
        }

        public bool CheckReference(string reference)
        {            
            var transactionRef = _context.Transactions.Where(x => x.TransactionReference == reference);
            if(transactionRef != null)
            {
                return false;
            }
            return true;
        }

        public bool CreateNewTopUp(string referenceNumber, int adminId, decimal amount)
        {
            try
            {
                var transaction = new TransactionEntity
                {
                    TransactionReference = referenceNumber,
                    TransactionType = EW_Enumerations.EW_TypeTransectionEnum.TopUp,
                    CustomerId = 0,
                    OtherId = adminId,
                    Status = false,
                    Amount = amount
                };
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CustomerTopup(int customerId, string referenceNumber)
        {
            try
            {
                TransactionEntity transaction = _context.Transactions.FirstOrDefault(x => x.TransactionReference == referenceNumber);
                transaction.CustomerId = customerId;
                transaction.Status = true;
                transaction.UpdateDateTime = DateTime.UtcNow;
                _context.Transactions.Update(transaction);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateNewPayment(int customerId, int merchantId, decimal amount, string referenceNumber)
        {
            try
            {
                var transaction = new TransactionEntity
                {
                    TransactionReference = referenceNumber,
                    TransactionType = EW_Enumerations.EW_TypeTransectionEnum.Payment,
                    CustomerId = customerId,
                    OtherId = merchantId,
                    Status = true,
                    Amount = amount
                };
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<TransactionEntity> GetTransactionByEmail(int customerId)
        {
            return _context.Transactions.Where(x => x.CustomerId == customerId).ToList();
        }

        public TransactionEntity GetTransactionByReferenceNumber(string referenceNumber)
        {
            return _context.Transactions.FirstOrDefault(x => x.TransactionReference == referenceNumber);
        }

        public TransactionEntity GetTransactionById(int transactionId)
        {
            return _context.Transactions.FirstOrDefault(x => x.Id == transactionId);
        }
    }
}
