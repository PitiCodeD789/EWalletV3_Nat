using System;
using System.Collections.Generic;
using System.Text;
using EWalletV2.Domain.Entities;

namespace EWalletV2.Domain.Repositories
{
    public interface IOtpRepository
    {
        bool SaveOtp(string email, string refOtp, string otpNumber);
        OtpEntity GetOtpByEmail(string email);
        void Delete(OtpEntity otpValidate);
    }
}
