using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.DtoModels.Auth
{
    public class LoginUserAndPassDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // Fixed Bug Get mobilephone match with entitymodel
        public string phoneNumber { get; set; }
        public string Account { get; set; }
    }
}
