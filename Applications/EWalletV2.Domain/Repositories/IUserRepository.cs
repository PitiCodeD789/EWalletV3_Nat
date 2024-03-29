﻿using EWalletV2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.Repositories
{
    public interface IUserRepository
    {
        UserEntity GetUserByEmail(string email);
        UserEntity GetUserByAccountNumber(string merchantAccNo);
        bool Update(UserEntity userData);
        bool ChangeBalance(string email, decimal amount);
        string AddUserAndGetAccount(UserEntity userData);
    }
}
