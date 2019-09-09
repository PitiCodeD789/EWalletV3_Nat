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
            var existingUser = _userRepository.GetUserByEmail(email);
            if(existingUser == null|| existingUser.BirthDate != birthday)
            {
                return false;
            }
            return true;
        }

        public bool ExistAccountNo(string merchantAccNo)
        {
            throw new NotImplementedException();
        }

        public bool ExistingEmail(string email)
        {
            var existingEmail = _userRepository.GetUserByEmail(email);
            return existingEmail != null;
        }

        public AccountViewModel GetAccountDetailByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public string GetAccountNameByAccountNumber(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public CheckPinDto GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(UpdateUserDtoCommand userDto)
        {
            throw new NotImplementedException();
        }
    }
}
