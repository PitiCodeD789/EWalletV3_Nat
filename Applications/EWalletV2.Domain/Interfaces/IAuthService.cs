using System;
using System.Collections.Generic;
using System.Text;
using EWalletV2.Domain.DtoModels.Auth;

namespace EWalletV2.Domain.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Save OTP number by Email
        /// </summary>
        /// <param name="email">Email (account) for save OTP</param>
        /// <returns>Reference number for re-check OTP</returns>
        string SaveOtp(string email);
        bool ValidateOtp(string email, string otp, string refNumber);
        string GetRefreshToken(string email);
        LoginUserAndPassDto LoginWithUsernameAndPassword(string username, string password);
        bool Logout(string email);
        string Register(RegisterDtoCommand registerDto);
        bool CheckPin(string pin, string email);
        bool UpdatePin(string email, string oldPin, string newPin);
        CheckPinDto GetUserByEmail(string email);
    }
}
