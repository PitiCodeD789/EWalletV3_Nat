using EWalletV2.Api.ViewModels;
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

        public TopupResultDTO GenerateTopUp(string account, decimal amount)
        {
            string referenceNumber = GenerateReferenceNumber(18);
            bool isReference = _transactionRepository.CheckReference(referenceNumber);
            // ??
            while (isReference)
            {
                referenceNumber = GenerateReferenceNumber(18);
                isReference = _transactionRepository.CheckReference(referenceNumber);
                if (!isReference)
                {
                    break;
                }
            }
            UserEntity userEntity = _userRepository.GetUserByAccountNumber(account);
            if (userEntity == null)
            {
                return null;
            }

            int otherId = userEntity.Id;
            bool isCreateTopUP = _transactionRepository.CreateNewTopUp(referenceNumber, otherId, amount);
            var topupData = _transactionRepository.GetTransactionByReferenceNumber(referenceNumber);
            TopupResultDTO returnData = new TopupResultDTO();
            returnData.ReferenceNumber = referenceNumber;
            returnData.ExpireDate = topupData.CreateDateTime.AddMinutes(15);
            return isCreateTopUP ? returnData : null;
        }

        private string GenerateReferenceNumber(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public List<TransactionDto> GetTransaction30Days(string email)
        {
            UserEntity userEntity = _userRepository.GetUserByEmail(email);
            int customerId = userEntity.Id;
            List<TransactionEntity> transactionEntities = _transactionRepository.Get30TransactionByCustomerId(customerId);
            if (transactionEntities == null)
            {
                return null;
            }

            List<TransactionDto> transactionDetails = new List<TransactionDto>();

            if (int.Parse(userEntity.Account.Substring(0, 2)) == (int)EW_Enumerations.EW_UserTypeEnum.Customer)
            {
                transactionDetails = transactionEntities.Select(x => new TransactionDto()
                {
                    TransactionId = x.Id,
                    TransactionReference = x.TransactionReference,
                    Account = x.UserOtherEntity.Account,
                    TransactionType = x.TransactionType,
                    FirstName = x.UserOtherEntity.FirstName,
                    LastName = x.UserOtherEntity.LastName,
                    Balance = x.Amount,
                    CreateDateTime = x.CreateDateTime
                }).ToList();
            }
            else
            {
                transactionDetails = transactionEntities.Select(x => new TransactionDto()
                {
                    TransactionId = x.Id,
                    Account = x.UserCustomerEntity.Account,
                    TransactionReference = x.TransactionReference,
                    TransactionType = x.TransactionType,
                    FirstName = x.UserCustomerEntity.FirstName,
                    LastName = x.UserCustomerEntity.LastName,
                    Balance = x.Amount,
                    CreateDateTime = x.CreateDateTime
                }).ToList();
            }

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
            if (customer == null)
                return null;

            UserEntity merchant = _userRepository.GetUserByAccountNumber(merchantAccNo);
            if (merchant == null)
                return null;

            int otherId = merchant.Id;
            int customerId = customer.Id;
            decimal amount = pay;

            if (amount > customer.Balance)
                return null;


            bool isPayment = _transactionRepository.CreateNewPayment(otherId, customerId, amount, referenceNumber);
            if (!isPayment)
                return null;

            TransactionEntity transactionEntity = _transactionRepository.GetTransactionByReferenceNumber(referenceNumber);
            if (transactionEntity == null)
                return null;

            amount = transactionEntity.Amount * (-1M);
            bool isChangeBalance = _userRepository.ChangeBalance(email, amount);
            if (!isChangeBalance)
                return null;

            isChangeBalance = _userRepository.ChangeBalance(merchant.Email, transactionEntity.Amount);
            if (!isChangeBalance)
                return null;

            var dto = new PaymentDto()
            {
                Reference = transactionEntity.TransactionReference,
                CreateDatetime = transactionEntity.CreateDateTime
            };

            return dto;
        }

        public TopupDto Topup(string email, string referenceNumber)
        {
            var userEntity = _userRepository.GetUserByEmail(email);
            if (userEntity == null)
            {
                return null;
            }

            int customerId = userEntity.Id;
            var transactionEntity = _transactionRepository.GetTransactionByReferenceNumber(referenceNumber);

            var expiredTime = transactionEntity?.CreateDateTime.AddMinutes(15);

            if (transactionEntity == null || transactionEntity.Status || expiredTime < DateTime.UtcNow)
                return null;

            //??
            bool isTopUp = _transactionRepository.CustomerTopup(customerId, referenceNumber);
            if (!isTopUp)
                return null;

            bool isChangeBalance = _userRepository.ChangeBalance(email, transactionEntity.Amount);
            if (!isChangeBalance)
                return null;

            var dto = new TopupDto()
            {
                IsSuccess = true
            };

            return dto;
        }

    }
}
