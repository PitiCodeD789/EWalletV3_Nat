using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Auth
{
    public class CheckPinCommand
    {
        public string Email { get; set; }
        public string Pin { get; set; }
    }
}
