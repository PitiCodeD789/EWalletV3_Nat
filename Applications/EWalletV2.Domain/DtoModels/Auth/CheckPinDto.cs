using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.DtoModels.Auth
{
    public class CheckPinDto
    {

        public string Email { get; set; }
        public string Pin { get; set; }
    }
}
