using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Api.ViewModels
{
    public static class EW_Enumerations
    {
        /// <summary>
        /// Mem = 0. Women = 1
        /// </summary>
        public enum EW_GenderEnum
        {
            Men = 0,
            Women = 1
        }

        /// <summary>
        /// Customer = 0, Merchant = 1, Admin = 2
        /// </summary>
        public enum EW_UserTypeEnum
        {
            Customer,
            Merchant,
            Admin
        }

        /// <summary>
        /// TopUp = 0, Payment = 1
        /// </summary>
        public enum EW_TypeTransectionEnum
        {
            TopUp,
            Payment
        }
    }
}
