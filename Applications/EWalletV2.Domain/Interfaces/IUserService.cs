using System;
using System.Collections.Generic;
using System.Text;
using EWalletV2.Domain.DtoModels.Auth;

namespace EWalletV2.Domain.Interfaces
{
    public interface IUserService
    {
        bool ExistingEmail(string email);

        CheckPinDto GetUserByEmail(string email);

        bool CheckUserByEmailAndBirthday(object email, object birthday);

    }
}
