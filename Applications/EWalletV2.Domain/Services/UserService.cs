using AutoMapper;
using EWalletV2.Api.ViewModels.User;
using EWalletV2.Domain.DtoModels.Auth;
using EWalletV2.Domain.DtoModels.User;
using EWalletV2.Domain.Interfaces;
using EWalletV2.Domain.Repoitories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.Services
{
    public class UserService :IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool CheckUserByEmailAndBirthday(string email, DateTime birthday)
        {
            var userData = _userRepository.GetUserByEmail(email);
            if(userData == null|| userData.BirthDate != birthday)
            {
                return false;
            }
            return true;
        }

        public bool ExistAccountNo(string merchantAccNo)
        {
            var existingAccountNo = _userRepository.GetUserByAccountNumber(merchantAccNo);
            return existingAccountNo != null;
        }

        public bool ExistingEmail(string email)
        {
            var existingEmail = _userRepository.GetUserByEmail(email);
            return existingEmail != null;
        }

        public AccountViewModel GetAccountDetailByEmail(string email)
        {
            var userData = _userRepository.GetUserByEmail(email);
            if(userData == null)
            {
                return null;
            }
            AccountViewModel accountDetail = new AccountViewModel()
            {
                AccountName = userData.FirstName + " " + userData.LastName,
                Balance = userData.Balance
            };
            return accountDetail;
        }

        public string GetAccountNameByAccountNumber(string accountNumber)
        {
            var userData = _userRepository.GetUserByAccountNumber(accountNumber);
            if(userData == null)
            {
                return null;
            }
            var accountName = userData.FirstName + " " + userData.LastName;
            return accountName;
        }

        public bool UpdateUser(UpdateUserDtoCommand userDto)
        {
            var userData = _userRepository.GetUserByEmail(userDto.Email);
            if(userData == null)
            {
                return false;
            }
            userData.FirstName = userDto.FirstName;
            userData.LastName = userDto.LastName;
            userData.MobileNumber = userDto.MobileNumber;
            userData.BirthDate = userDto.BitrhDate;
            userData.Gender = userDto.Gender;
            userData.UpdateDateTime = DateTime.UtcNow;
            bool result = _userRepository.Update(userData);
            return result;
        }
    }
}
