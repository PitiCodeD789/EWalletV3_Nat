using EV.Service.Services;
using EWalletV2.Api.ViewModels.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EV.Admin.ViewModels
{
    public class GenerateQRcodeViewModel :BaseViewModel
    {

        public GenerateQRcodeViewModel(GenerateTopupViewModel generateTopup)
        {
            var topupJson = JsonConvert.SerializeObject(generateTopup);
            QrcodeData = topupJson;
        }
        private string qrcodeData;

        public string QrcodeData
        {
            get { return qrcodeData; }
            set { qrcodeData = value;
                OnPropertyCHanged();
            }
        }

    }
}
