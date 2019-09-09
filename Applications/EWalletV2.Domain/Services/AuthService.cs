using AutoMapper;
using EWalletV2.Domain.DtoModels.Auth;
using EWalletV2.Domain.Entity;
using EWalletV2.Domain.Helpers;
using EWalletV2.Domain.Interfaces;
using EWalletV2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EWalletV2.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IOtpRepository _otpRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IMapper _mapper;

        public AuthService(IOtpRepository otpRepository , IUserRepository userRepository, ITokenRepository tokenRepository, IMapper mapper )
        {
            _otpRepository = otpRepository;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _mapper = mapper;
        }

        public bool CheckPin(string pin, string email)
        {

            UserEntity user = _userRepository.GetUserByEmail(email);

            if(user == null)
            {
                return false;
            }

            string pass = Generator.HashPassword(pin, user.Salt);
            if(pass == user.Pin)
            {
                return true;
            }

            return false;


        }

        public string GetRefreshToken(string email)
        {
            string refreshToken = Generator.GenerateRandomString(12);
            TokenEntity tokenEntity = _tokenRepository.GetTokenByEmail(email);

            if (tokenEntity == null)
            {
                tokenEntity = new TokenEntity()
                {
                    Email = email,
                    RefreshToken = refreshToken
                };
                _tokenRepository.AddToken(tokenEntity);

            }
            else
            {
                tokenEntity.RefreshToken = refreshToken;
                _tokenRepository.UpdateToken(tokenEntity);
            }

            return tokenEntity.RefreshToken;
        }

        public LoginUserAndPassDto LoginWithUsernameAndPassword(string username, string password)
        {
            UserEntity user = _userRepository.GetUserByEmail(username);
            if (user == null)
                return null;
            string checkPassword = Generator.HashPassword(password, user.Salt);
            if (checkPassword == user.Pin)
            {
                LoginUserAndPassDto loginUser = _mapper.Map<LoginUserAndPassDto>(user);
                return loginUser;
            }

            return null;
        }

        public bool Logout(string email)
        {
            bool result = _tokenRepository.DeleteTokenByEmail(email);
            return result;
        }

        public string Register(RegisterDtoCommand registerDto)
        {
            UserEntity userEntity = _mapper.Map<UserEntity>(registerDto);
            userEntity.Salt = Generator.GenerateRandomString(12);
            userEntity.Pin = Generator.HashPassword(registerDto.Pin, userEntity.Salt);
            string account = _userRepository.AddUserAndGetAccount(userEntity);
            return account;
        }

        public string SaveOtp(string email)
        {
            string refOtp = Generator.GenerateRandomString(6);
            string otpNumber = Generator.GenerateOtp();
            bool isSaveOtp = _otpRepository.SaveOtp(email, refOtp, otpNumber);
            if (!isSaveOtp)
                return null;

            Task.Run(() => { SendEmail(email, refOtp, otpNumber); });
            return refOtp;
        }

        private void SendEmail(string email, string refOtp, string otpNumber)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtpm.csloxinfo.com");

                mail.From = new MailAddress("student@enixer.net");
                mail.To.Add(email);
                mail.Subject = @"Otp Number";
                mail.Body = $"ref : {otpNumber}, otp: {refOtp}";
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("student@enixer.net", "Gg123456789");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                SmtpServer.Dispose();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Send Email =" + ex.ToString());
            }

        }


        public bool UpdatePin(string email, string oldPin, string newPin)
        {
            UserEntity user = _userRepository.GetUserByEmail(email);
            if(user == null)
            {
                return false;
            }

            string checkPassword = Generator.HashPassword(oldPin, user.Salt);
            if (checkPassword == user.Pin)
            {
                //LoginUserAndPassDto loginUser = _mapper.Map<LoginUserAndPassDto>(user);
                string salt = Generator.GenerateRandomString(12);
                string hashPass = Generator.HashPassword(newPin, salt);
                user.Pin = hashPass;
                user.Salt = salt;
                bool isUpdate = _userRepository.Update(user);
                return isUpdate;
            }
            return false;


        }

        public bool ValidateOtp(string email, string otp, string refNumber)
        {
            OtpEntity otpValidate = _otpRepository.GetOtpByEmail(email);
            if (otpValidate == null || (otpValidate.OTP != otp || otpValidate.Reference != refNumber))
                return false;
            _otpRepository.Delete(otpValidate);
            if (otpValidate.CreateDate > DateTime.UtcNow.AddMinutes(-15))
            {
                return false;
            }
            return true;
        }

        public bool GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
