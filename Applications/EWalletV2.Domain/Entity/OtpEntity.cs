using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.Entity
{
  public  class OtpEntity
    {

        public string OTP { get; internal set; }
        public string Reference { get; internal set; }
        public DateTime CreateDate { get; internal set; }
    }
}
