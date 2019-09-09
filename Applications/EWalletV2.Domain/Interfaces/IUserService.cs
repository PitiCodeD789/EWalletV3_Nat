using EWalletV2.Domain.DtoModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.Interfaces
{
    public interface IUserService
    {
        bool ExistingEmail(string email);
<<<<<<< HEAD
        bool UpdateUser(UpdateUserDtoCommand userDto);
=======
        string GetAccountNameByAccountNumber(string accountNumber);
>>>>>>> 33d3523f7c7c8b8a9cf7a478b01df41e8f7c588e
    }
}
