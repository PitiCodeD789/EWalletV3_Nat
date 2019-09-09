using System;
using System.Collections.Generic;
using System.Text;
using EWalletV2.Domain.DtoModels.Auth;

namespace EWalletV2.Domain.Interfaces
{
    public interface IUserService
    {
        bool ExistingEmail(string email);
<<<<<<< HEAD
        CheckPinDto GetUserByEmail(string email);
=======
        bool CheckUserByEmailAndBirthday(object email, object birthday);
>>>>>>> 516967869bc1b0571a34766a786131b669d93922
    }
}
