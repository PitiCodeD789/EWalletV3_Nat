using EWalletV2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.Repositories
{
    public interface IUserRepository
    {
        UserEntity GetUserByEmail(string email);
        string AddUserAndGetAccount(UserEntity userEntity);
        bool Update(UserEntity user);
    }
}
