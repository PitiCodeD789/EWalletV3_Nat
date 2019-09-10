using EV.Service.Models;
using EWalletV2.Api.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EV.Service.Interfaces
{
   public interface IAuthService
    {

        Task<ResultServiceModel<CheckEmailViewModel>> SignIn(string email);
        Task<ResultServiceModel<CheckOtpViewModel>> CheckOtp(string email, string otp, string refNumber);
        Task<ResultServiceModel<RegisterViewModel>> Register(RegisterCommand register);
        Task<ResultServiceModel<GetTokenByRefreshTokenViewModel>> GetTokenByRefreshToken(string email, string refreshToken);
        Task<ResultServiceModel<LoginUserAndPassViewModel>> LoginUserAndPass(string username, string password);
        Task<ResultServiceModel<DummyModel>> Logout(string email);
    }
}
