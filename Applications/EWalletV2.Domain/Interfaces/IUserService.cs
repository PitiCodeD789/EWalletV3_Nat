using EWalletV2.Domain.DtoModels.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.Interfaces
{
    public interface IUserService
    {
        bool ExistingEmail(string email);
        bool UpdateUser(UpdateUserDtoCommand userDto);
    }
}
