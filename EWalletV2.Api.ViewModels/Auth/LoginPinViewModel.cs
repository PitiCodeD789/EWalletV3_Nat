using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Auth
{
   public class LoginPinViewModel
    {
        public string RefreshToken { get; set; }
        public string Account { get; set; }
        public string Token { get; set; }
    }
}
