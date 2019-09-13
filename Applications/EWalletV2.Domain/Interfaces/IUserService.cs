using EWalletV2.Api.ViewModels.User;
using EWalletV2.Domain.DtoModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using EWalletV2.Domain.DtoModels.Auth;

namespace EWalletV2.Domain.Interfaces
{
    public interface IUserService
    {
        bool ExistingEmail(string email);
        bool UpdateUser(UpdateUserDtoCommand userDto);
        string GetAccountNameByAccountNumber(string accountNumber);
        AccountViewModel GetAccountDetailByEmail(string email);
        bool ExistAccountNo(string merchantAccNo);
        bool CheckUserByEmailAndBirthday(string email, DateTime birthday);

    }
}
