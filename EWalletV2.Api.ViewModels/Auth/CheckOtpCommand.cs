using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Auth
{
    public class CheckOtpCommand
    {
        public string Email { get; set; }
        public string Otp { get; set; }
        public string RefNumber { get; set; }
    }
}
