using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.DtoModels.Transaction
{
    public class TopupDto
    {
        public decimal Amount { get; set; }
        public bool IsTopupExist { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsExpired { get; set; }
    }
}
