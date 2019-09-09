using System;
using System.Collections.Generic;
using System.Text;
using EWalletV2.Api.ViewModels.User;

namespace EWalletV2.Domain.Interfaces
{
    public interface IUserService
    {
        bool ExistingEmail(string email);
        AccountViewModel GetAccountDetailByEmail(string email);
    }
}
