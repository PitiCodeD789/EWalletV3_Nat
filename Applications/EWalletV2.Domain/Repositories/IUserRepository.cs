using System;
using System.Collections.Generic;
using System.Text;
using EWalletV2.Domain.Entity;

namespace EWalletV2.Domain.Repositories
{
    public interface IUserRepository
    {
        UserEntity GetUserByEmail(string email);
        string AddUserAndGetAccount(UserEntity userEntity);
        bool Update(UserEntity user);
    }
}
