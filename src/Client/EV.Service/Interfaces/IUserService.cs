using EV.Service.Models;
using EWalletV2.Api.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EV.Service.Interfaces
{
    public interface IUserService
    {
        Task<ResultServiceModel<AccountViewModel>> GetUser(string email);
        Task<ResultServiceModel<AccountViewModel>> GetBalance(string email);
        Task<ResultServiceModel<AccountViewModel>> GetAccount(string accountNumber);
        Task<ResultServiceModel<DummyModel>> UpdateUser(UpdateUserCommand updateUserCommand);
    }
}
