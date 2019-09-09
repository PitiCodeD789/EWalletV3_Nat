using AutoMapper;
using EWalletV2.Domain.DtoModels.Auth;
using EWalletV2.Domain.Interfaces;
using EWalletV2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
