using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Auth
{
    public class CheckEmailViewModel
    {
        public bool IsExist { get; set; }
        public string RefNumber { get; set; }
        public EW_Enumerations.EW_UserTypeEnum Role { get; set; }
    }
}
