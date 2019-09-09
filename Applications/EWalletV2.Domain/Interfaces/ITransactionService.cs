<<<<<<< HEAD
﻿using EWalletV2.Domain.DtoModels.Transaction;
using System;
using System.Collections.Generic;
using System.Text;
=======
﻿using System;
using System.Collections.Generic;
using System.Text;
using EWalletV2.Domain.DtoModels.Transaction;
>>>>>>> controller_transaction_payment

namespace EWalletV2.Domain.Interfaces
{
    public interface ITransactionService
    {
<<<<<<< HEAD
        TopupDto Topup(string email, string referenceNumber);
        string GenerateTopUp(string email, string amount);
=======
        PaymentDto Payment(string email, string merchantAccNo, decimal pay);
>>>>>>> controller_transaction_payment
    }
}
