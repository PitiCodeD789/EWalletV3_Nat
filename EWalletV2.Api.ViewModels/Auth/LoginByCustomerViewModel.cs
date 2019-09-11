﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Auth
{
    public class LoginByCustomerViewModel
    {
        public string RefreshToken { get; set; }
        public string Token { get; set; }
        public string Account { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string MobileNumber { get; set; }
        public EW_Enumerations.EW_GenderEnum Gender { get; set; }
    }
}
