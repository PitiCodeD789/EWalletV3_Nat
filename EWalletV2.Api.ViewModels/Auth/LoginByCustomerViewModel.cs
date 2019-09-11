using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Auth
{
    public class LoginByCustomerViewModel
    {
        public string RefreshToken { get; set; }
        public string Token { get; set; }
        public string Account { get; set; }
    }
}
