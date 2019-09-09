using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.Entities
{
    public class TokenEntity : BaseEntity
    {
        public string RefreshToken { get; set; }
        public string Email { get; set; }
    }
}
