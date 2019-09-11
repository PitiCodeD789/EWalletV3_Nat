using EV.Service.Models;
using EWalletV2.Api.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EV.Service.Interfaces
{
    public interface ITransactionServices
    {
        Task<ResultServiceModel<List<TransactionViewModel>>> GetTransaction30Days(string email);
        Task<ResultServiceModel<PaymentViewModel>> Payment(string email, string merchantAccountNo, decimal pay);
        Task<ResultServiceModel<TopupViewModel>> Topup(string email, string referenceNumber);
        Task<ResultServiceModel<GenerateTopupViewModel>> GenerateTopup(string account, decimal amount);
    }
}
