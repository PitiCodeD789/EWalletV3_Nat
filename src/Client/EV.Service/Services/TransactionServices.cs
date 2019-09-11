using EV.Service.Interfaces;
using EV.Service.Models;
using EWalletV2.Api.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EV.Service.Services
{
    public class TransactionServices : BaseService, ITransactionServices
    {
        private string serviceUrl = Helper.BaseUrl + "transaction/";

        public async Task<ResultServiceModel<List<TransactionViewModel>>> GetTransaction30Days(string email)
        {
            string url = serviceUrl + "GetListTransaction/" + email;
            return await Get<List<TransactionViewModel>>(url);
        }

        public async Task<ResultServiceModel<PaymentViewModel>> Payment(string email, string merchantAccountNo, decimal pay)
        {
            PaymentCommand model = new PaymentCommand()
            {
                Email = email,
                MerchantAccountNo = merchantAccountNo,
                Pay = pay
            };

            string url = serviceUrl + "payment";
            return await Post<PaymentViewModel>(url, model);
        }

        public async Task<ResultServiceModel<TopupViewModel>> Topup(string email, string referenceNumber)
        {
            TopupCommand model = new TopupCommand()
            {
                Email = email,
                ReferenceNumber = referenceNumber
            };

            string url = serviceUrl + "Topup";
            return await Post<TopupViewModel>(url, model);
        }

        public async Task<ResultServiceModel<GenerateTopupViewModel>> GenerateTopup(string account, decimal amount)
        {
            GenerateTopupCommand model = new GenerateTopupCommand()
            {
                Account = account,
                Amount = amount
            };

            string url = serviceUrl + "GenerateTopup";
            return await Post<GenerateTopupViewModel>(url, model);
        }
    }
}
