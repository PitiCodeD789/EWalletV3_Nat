using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels.Auth
{
    public class RegisterCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string MobileNumber { get; set; }
        public EW_Enumerations.EW_GenderEnum Gender { get; set; }
        public string Email { get; set; }
        public string Pin { get; set; }
    }
}
