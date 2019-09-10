using EV.Service.Models;
using EWalletV2.Api.ViewModels.Pin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EV.Service.Interfaces
{
    public interface IPinService
    {
        Task<ResultServiceModel<LoginByPinViewModel>> LoginByPin(string pin, string email);
        Task<ResultServiceModel<DummyModel>> CheckPin(string pin, string email);
        Task<ResultServiceModel<DummyModel>> UpdatePin(string newPin, string oldPin, string email);
        Task<ResultServiceModel<string>> CheckForgotPin(DateTime birthday, string email);
    }
}
