using EWalletV2.Domain.DtoModels.Auth;
using EWalletV2.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.Services
{
    public class AuthService : IAuthService
    {

        

        public bool CheckPin(string pin, string email)
        {



            return true;


        }

        public string GetRefreshToken(string email)
        {
            throw new NotImplementedException();
        }

        public LoginUserAndPassDto LoginWithUsernameAndPassword(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool Logout(string email)
        {
            throw new NotImplementedException();
        }

        public string Register(RegisterDtoCommand registerDto)
        {
            throw new NotImplementedException();
        }

        public string SaveOtp(string email)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePin(string email, string oldPin, string newPin)
        {
            throw new NotImplementedException();
        }

        public bool ValidateOtp(string email, string otp, string refNumber)
        {
            throw new NotImplementedException();
        }
    }
}
