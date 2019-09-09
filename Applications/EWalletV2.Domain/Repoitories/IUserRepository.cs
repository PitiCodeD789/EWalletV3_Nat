using EWalletV2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.Repoitories
{
    public interface IUserRepository
    {
        UserEntity GetUserByEmail(string email);
        UserEntity GetUserByAccountNumber(string merchantAccNo);
        bool Update(UserEntity userData);
    }
}
