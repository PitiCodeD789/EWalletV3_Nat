using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Auth
{
    public class CheckPinCommand
    {
        public object Email { get; set; }
        public object Pin { get; set; }
    }
}
