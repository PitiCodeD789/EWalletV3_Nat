using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.Interfaces
{
    public interface IUserService
    {
        bool ExistingEmail(string email);
        string GetAccountNameByAccountNumber(string accountNumber);
    }
}
