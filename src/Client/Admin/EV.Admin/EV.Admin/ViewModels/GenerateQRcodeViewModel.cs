using EV.Admin.Views;
using EV.Service.Services;
using EWalletV2.Api.ViewModels.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Admin.ViewModels
{
    public class GenerateQRcodeViewModel :BaseViewModel
    {
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
        private GenerateTopupViewModel displayData;

        public GenerateTopupViewModel DisplayData
        {
            get { return displayData; }
            set { displayData = value;
                OnPropertyChanged();
            }
        }

        public ICommand BacktoPreviousCommand { get; set; }
        public GenerateQRcodeViewModel()
        {
            BacktoPreviousCommand = new Command(Goback);
        }
       
        private async void Goback()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AdminTabbedPage());
        }

    }
}
