using EV.Service.Interfaces;
using EV.Service.Models;
using EWalletV2.Api.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EV.Service.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private string serviceUrl = Helper.BaseUrl + "auth/";
        public async Task<ResultServiceModel<CheckOtpViewModel>> CheckOtp(string email, string otp, string refNumber)
        {
            string url = Helper.BaseUrl + "auth/CheckOtp";
            CheckOtpCommand otpCommand = new CheckOtpCommand
            {
                Email = email,
                Otp = otp,
                RefNumber = refNumber
            };

            return await Post<CheckOtpViewModel>(url, otpCommand);

        }

        public async Task<ResultServiceModel<GetTokenByRefreshTokenViewModel>> GetTokenByRefreshToken(string email, string refreshToken)
        {

            string url = Helper.BaseUrl + "auth/GetTokenByRefreshToken";

            GetTokenByRefreshTokenCommand tokenCommand = new GetTokenByRefreshTokenCommand
            {
                Email = email,

                RefreshToken = refreshToken

            };

            return await Post<GetTokenByRefreshTokenViewModel>(url, tokenCommand);

        }

        public async Task<ResultServiceModel<LoginUserAndPassViewModel>> LoginUserAndPass(string username, string password)
        {
            try
            {
                string url = Helper.BaseUrl + "auth/Login";

                LoginUserAndPassCommand loginUser = new LoginUserAndPassCommand { Username = username, Password = password };

                return await Post<LoginUserAndPassViewModel>(url, loginUser);
            }
            catch(Exception e)
            {
                return null;
            }

        }

        public async Task<ResultServiceModel<DummyModel>> Logout(string email)
        {
            string url = Helper.BaseUrl + "auth/Logout?email=" +email;
            return await Get<DummyModel>(url);
        }

        public async Task<ResultServiceModel<RegisterViewModel>> Register(RegisterCommand register)
        {
            string url = Helper.BaseUrl + "auth/Register";
            return await Post<RegisterViewModel>(url, register);
        }

        public async Task<ResultServiceModel<CheckEmailViewModel>> SignIn(string email)
        {
            string url = Helper.BaseUrl + "auth/CheckEmail/" + email;
            return await Get<CheckEmailViewModel>(url);
        }

        public async Task<ResultServiceModel<LoginByCustomerViewModel>> LoginByCustomer(string email, string pin)
        {
            LoginByCustomerCommand model = new LoginByCustomerCommand()
            {
                Email = email,
                Pin = pin
            };

            string url = serviceUrl + "LoginByCustomer";
            return await Post<LoginByCustomerViewModel>(url, model);
        }
    }
}
