using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.DtoModels.Transaction
{
    public class PaymentDto
    {
        public string Reference { get; set; }
        public DateTime CreateDatetime { get; set; }
    }
}
