using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Auth
{
    public class LoginUserAndPassViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string RefreshToken { get; set; }
        public string Account { get; set; }
        public string Token { get; set; }
    }
}
