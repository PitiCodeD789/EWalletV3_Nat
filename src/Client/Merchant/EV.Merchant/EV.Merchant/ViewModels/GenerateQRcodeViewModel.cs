using EV.Service.Services;
using EWalletV2.Api.ViewModels.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EV.Merchant.ViewModels
{
    public class GenerateQRcodeViewModel : BaseViewModel
    {
        public GenerateQRcodeViewModel(GeneratePaymentViewModel generateTopup)
        {
            var topupJson = JsonConvert.SerializeObject(generateTopup);
            QrcodeData = topupJson;
        }
        private string qrcodeData;

        public string QrcodeData
        {
            get { return qrcodeData; }
            set
            {
                qrcodeData = value;
                OnPropertyChanged();
            }
        }
    }
}
