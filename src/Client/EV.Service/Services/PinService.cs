using EV.Service.Interfaces;
using EV.Service.Models;
using EWalletV2.Api.ViewModels.Auth;
using EWalletV2.Api.ViewModels.Pin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EV.Service.Services
{
    public class PinService : BaseService, IPinService
    {
        private string serviceUrl = Constant.BaseUrl + "pin/";

        public async Task<ResultServiceModel<LoginByPinViewModel>> LoginByPin(string pin, string email)
        {
            LoginPinCommand model = new LoginPinCommand()
            {
                Pin = pin,
                Email = email
            };

            string url = serviceUrl + "LoginByPin";
            return await Post<LoginByPinViewModel>(url, model);
        }

        public async Task<ResultServiceModel<NullModel>> CheckPin(string pin, string email)
        {
            CheckPinCommand model = new CheckPinCommand()
            {
                Pin = pin,
                Email = email
            };

            string url = serviceUrl + "CheckPin";
            return await Post<NullModel>(url, model);
        }

        public async Task<ResultServiceModel<NullModel>> UpdatePin(string newPin, string oldPin, string email)
        {
            UpdatePinCommand model = new UpdatePinCommand()
            {
                NewPin = newPin,
                OldPin = oldPin,
                Email = email
            };

            string url = serviceUrl + "updatepin";
            return await Post<NullModel>(url, model);
        }

        public async Task<ResultServiceModel<string>> CheckForgotPin(DateTime birthday, string email)
        {
            CheckForgotPinCommand model = new CheckForgotPinCommand()
            {
                Birthday = birthday,
                Email = email
            };

            string url = serviceUrl + "CheckForgotPin";
            return await Post<string>(url, model);
        }
    }
}
