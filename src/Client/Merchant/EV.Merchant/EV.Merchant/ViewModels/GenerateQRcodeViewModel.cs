using EV.Service.Services;
using EWalletV2.Api.ViewModels.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EV.Merchant.ViewModels
{
    public class GenerateQRcodeViewModel : BaseViewModel
    {
        public ICommand BacktoPreviousCommand { get; set; }
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
        private string fullname;

        public string FullName
        {
            get { return fullname; }
            set
            {
                fullname = value;
                OnPropertyChanged();
            }
        }
        public GenerateQRcodeViewModel()
        {
            FullName = SecureStorage.GetAsync("StoreName").Result;
            BacktoPreviousCommand = new Command(Goback);
        }
        private async void Goback()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
