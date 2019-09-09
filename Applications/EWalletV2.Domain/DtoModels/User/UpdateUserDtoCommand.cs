using EWalletV2.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.DtoModels.User
{
    public class UpdateUserDtoCommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BitrhDate { get; set; }
        public string MobileNumber { get; set; }
        public EW_Enumerations.EW_GenderEnum Gender { get; set; }
        public string Email { get; set; }
    }
}
