using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.Entities
{
    public class OtpEntity : BaseEntity
    {
        public string Otp { get; set; }
        public string Reference { get; set; }
        public string Email { get; set; }
    }
}
