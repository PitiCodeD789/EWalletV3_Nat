using EV.Service.Services;
using EWalletV2.Api.ViewModels.Transaction;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EV.Admin.ViewModels
{
    public class InputMoneyViewModel : BaseViewModel
    {
        private readonly TransactionServices _transactionService;
        private decimal amount;
        public ICommand BacktoPreviousCommand { get; set; }
        public ICommand GenerateQRcodeCommand { get; set; }
        public decimal Amount
        {
            get { return amount; }
            set
            {
                amount = value; ;
            }
        }
        private GenerateTopupViewModel resultData;

        public GenerateTopupViewModel ResultData
        {
            get { return resultData; }
            set { resultData = value; }
        }

        public InputMoneyViewModel()
        {
            BacktoPreviousCommand = new Command(Goback);
            GenerateQRcodeCommand = new Command(GenerateQrcode);
        }

        private async void GenerateQrcode()
        {
            var email = await SecureStorage.GetAsync("Username");
            var resultData = await _transactionService.GenerateTopup(email, Amount);
            if (resultData.IsError == true)
            {
                await Application.Current.MainPage.DisplayAlert("","Error","ok");
            }
            else
            {
                //await Application.Current.MainPage.Navigation.PushAsync(new Test(resultData.Model));
            }
        }

        private async void Goback()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
