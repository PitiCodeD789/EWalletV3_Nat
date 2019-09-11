using EV.Service.Interfaces;
using EV.Service.Models;
using EWalletV2.Api.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EV.Service.Services
{
    public class UserService : BaseService, IUserService
    {
        public async Task<ResultServiceModel<AccountViewModel>> GetUser(string email)
        {
            string url = Helper.BaseUrl + "GetUser/" + email;
            return await Get<AccountViewModel>(url);
        }

        public async Task<ResultServiceModel<AccountViewModel>> GetBalance(string email)
        {
            EmailModel emailModel = new EmailModel()
            {
                Email = email
            };
            string url = Helper.BaseUrl + "GetBalance";
            return await Post<AccountViewModel>(url, emailModel);
        }

        public async Task<ResultServiceModel<AccountViewModel>> GetAccount(string accountNumber)
        {
            AccountCommand accountCommand = new AccountCommand()
            {
                AccountNumber = accountNumber
            };
            string url = Helper.BaseUrl + "GetAccount";
            return await Post<AccountViewModel>(url, accountCommand);
        }

        public async Task<ResultServiceModel<DummyModel>> UpdateUser(UpdateUserCommand updateUserCommand)
        {
            string url = Helper.BaseUrl + "UpdateUser";
            return await Post<DummyModel>(url, updateUserCommand);
        }

    }
}
