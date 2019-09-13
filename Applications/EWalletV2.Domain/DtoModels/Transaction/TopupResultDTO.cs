using System;
using System.Collections.Generic;
using System.Text;

namespace EWalletV2.Domain.DtoModels.Transaction
{
    public class TopupResultDTO
    {
        public string ReferenceNumber { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
