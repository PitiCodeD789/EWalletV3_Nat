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
            int otherId = userEntity.Id;
            bool isCreateTopUP = _transactionRepository.CreatNewTopUp(referenceNumber, otherId, amount);
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
            List<TransactionDetailDto> transactionDetails = transactionEntities.Select(x => new TransactionDetailDto()
            {

            }).ToList();
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

            bool isPayment = _transactionRepository.CreatNewPayment(otherId, customerId, amount, referenceNumber);
            if (!isPayment)
            {
                return null;
            }
            TransactionEntity transactionEntity = _transactionRepository.GetTransactionByReferenceNumber(referenceNumber);
            if(transactionEntity == null)
            {
                return null;
            }
            return new PaymentDto()
            {
                Reference = transactionEntity.Reference,
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
                    IsTopupExist = false,
                    IsSuccess = true,
                    IsExpired = false
                };
            }
            else
            {
                bool isTopUp = _transactionRepository.CustomerTopup(customerId, referenceNumber);
                if (!isTopUp)
                {
                    return new TopupDto()
                    {
                        IsTopupExist = false,
                        IsSuccess = false,
                        IsExpired = true
                    };
                }
                return new TopupDto()
                {
                    IsTopupExist = true,
                    IsSuccess = true,
                    IsExpired = true
                };
            }
            
        }

        public TransactionDto GetDetailTransaction(string email, int transactionId)
        {
            TransactionEntity transactionEntity = _transactionRepository.GetTransactionByTransactionId(transactionId);
            if(transactionEntity == null)
            {
                return null;
            }
        }
    }
}
