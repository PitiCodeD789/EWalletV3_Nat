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
        public ICommand BacktoPreviousCommand { get; set; }
        public GenerateQRcodeViewModel(GenerateTopupViewModel generateTopup)
        {
            var topupJson = JsonConvert.SerializeObject(generateTopup);
            QrcodeData = topupJson;
            BacktoPreviousCommand = new Command(Goback);
        }
        private string qrcodeData;

        public string QrcodeData
        {
            get { return qrcodeData; }
            set { qrcodeData = value;
                OnPropertyCHanged();
            }
        }
        private async void Goback()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
