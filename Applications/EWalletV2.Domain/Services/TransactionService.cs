using EWalletV2.Domain.DtoModels.Transaction;
using EWalletV2.Domain.Entities;
using EWalletV2.Domain.Interfaces;
using EWalletV2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EWalletV2.Domain.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;
        public TransactionService(IUserRepository userRepository, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        public string GenerateTopUp(string email, string amount)
        {
            string referenceNumber = GenerateReferenceNumber(18);
            bool isReference = _transactionRepository.CheckReference(referenceNumber);
            while (isReference)
            {
                referenceNumber = GenerateReferenceNumber(18);
                isReference = _transactionRepository.CheckReference(referenceNumber);
                if (!isReference)
                {
                    break;
                }
            }
            UserEntity userEntity = _userRepository.GetUserByEmail(email);
            decimal deAmount = Decimal.Parse(amount);
            int otherId = userEntity.Id;
            bool isCreateTopUP = _transactionRepository.CreateNewTopUp(referenceNumber, otherId, deAmount);
            if (!isCreateTopUP)
            {
                return null;
            }
            return referenceNumber;
        }

        private string GenerateReferenceNumber(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public List<TransactionDetailDto> GetTransaction30Days(string email)
        {
            UserEntity userEntity = _userRepository.GetUserByEmail(email);
            int customerId = userEntity.Id;
            List<TransactionEntity> transactionEntities = _transactionRepository.GetTransactionByCustomerId(customerId);
            if(transactionEntities == null)
            {
                return null;
            }
            string fullName = userEntity.FirstName + " " + userEntity.LastName;
            List<TransactionDetailDto> transactionDetails = transactionEntities.Select(x => new TransactionDetailDto()
            {
                TransactionId = x.Id,
                TransactionType = x.TransactionType,
                Name = fullName,
                Balance = x.Amount,
                CreateDate = x.CreateDateTime
            }).ToList();
            return transactionDetails;
        }

        public PaymentDto Payment(string email, string merchantAccNo, decimal pay)
        {
            string referenceNumber = GenerateReferenceNumber(18);
            bool isReference = _transactionRepository.CheckReference(referenceNumber);
            while (isReference)
            {
                referenceNumber = GenerateReferenceNumber(18);
                isReference = _transactionRepository.CheckReference(referenceNumber);
                if (!isReference)
                {
                    break;
                }
            }
            UserEntity customer = _userRepository.GetUserByEmail(email);
            UserEntity merchant = _userRepository.GetUserByAccountNumber(merchantAccNo);
            int otherId = merchant.Id;
            int customerId = customer.Id;
            decimal amount = pay;
            if(amount < customer.Balance)
            {
                return null;
            }
            bool isPayment = _transactionRepository.CreateNewPayment(otherId, customerId, amount, referenceNumber);
            if (!isPayment)
            {
                return null;
            }
            TransactionEntity transactionEntity = _transactionRepository.GetTransactionByReferenceNumber(referenceNumber);
            if(transactionEntity == null)
            {
                return null;
            }
            amount = transactionEntity.Amount * (-1M);
            bool isChangeBalance = _userRepository.ChangeBalance(email, amount);
            if (!isChangeBalance)
            {
                return null;
            }
            return new PaymentDto()
            {
                Reference = transactionEntity.TransactionReference,
                CreateDatetime = transactionEntity.CreateDateTime
            };
        }

        public TopupDto Topup(string email, string referenceNumber)
        {
            UserEntity userEntity = _userRepository.GetUserByEmail(email);
            int customerId = userEntity.Id;
            TransactionEntity transactionEntity = _transactionRepository.GetTransactionByReferenceNumber(referenceNumber);
            if(transactionEntity == null)
            {
                return new TopupDto()
                {
                    IsTopupExist = false,
                    IsSuccess = false,
                    IsExpired = false
                };
            }
            else if (transactionEntity.CreateDateTime.AddMinutes(15) < DateTime.UtcNow)
            {
                return new TopupDto()
                {
                    IsTopupExist = true,
                    IsSuccess = false,
                    IsExpired = true
                };
            }
            else
            {
                bool isTopUp = _transactionRepository.CustomerTopup(customerId, referenceNumber);
                if (!isTopUp)
                {
                    return new TopupDto()
                    {
                        IsTopupExist = true,
                        IsSuccess = false,
                        IsExpired = false
                    };
                }
                decimal amount = transactionEntity.Amount;
                bool isChangeBalance = _userRepository.ChangeBalance(email, amount);
                if (!isChangeBalance)
                {
                    return new TopupDto()
                    {
                        IsTopupExist = true,
                        IsSuccess = false,
                        IsExpired = false
                    };
                }
                return new TopupDto()
                {
                    IsTopupExist = true,
                    IsSuccess = true,
                    IsExpired = false
                };
            }
            
        }

        public TransactionDto GetDetailTransaction(string email, int transactionId)
        {
            UserEntity userEntity = _userRepository.GetUserByEmail(email);
            TransactionEntity transactionEntity = _transactionRepository.GetTransactionByTransactionId(transactionId);
            if(transactionEntity == null)
            {
                return null;
            }
            string fullName = userEntity.FirstName + " " + userEntity.LastName;
            TransactionDto transaction = new TransactionDto()
            {
                TransactionId = transactionEntity.Id,
                TransactionType = transactionEntity.TransactionType,
                Name = fullName,
                Balance = transactionEntity.Amount,
                AccountNo = userEntity.Account
            };
            return transaction;
        }
    }
}
