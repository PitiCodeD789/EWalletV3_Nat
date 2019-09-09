using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.Entity
{
   public class TokenEntity
    {
        public string Email { get; internal set; }
        public string RefreshToken { get; internal set; }
    }
}
