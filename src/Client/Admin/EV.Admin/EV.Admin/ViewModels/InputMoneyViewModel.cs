using EV.Admin.Views;
using EV.Service.Services;
using EWalletV2.Api.ViewModels.Transaction;
using Rg.Plugins.Popup.Services;
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
        public ICommand BacktoPreviousCommand { get; set; }
        public ICommand GenerateQRcodeCommand { get; set; }
        private decimal amount;
        public decimal Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }
        private GenerateTopupViewModel resultData;

        public GenerateTopupViewModel ResultData
        {
            get { return resultData; }
            set { resultData = value;
                OnPropertyChanged();
            }
        }

        public InputMoneyViewModel()
        {
            _transactionService = new TransactionServices();
            BacktoPreviousCommand = new Command(Goback);
            GenerateQRcodeCommand = new Command(GenerateQrcode);
        }

        private async void GenerateQrcode()
        {
            var accountNumber = await SecureStorage.GetAsync("Account");
            var callData = await _transactionService.GenerateTopup(accountNumber, Amount);
            var refNumber = callData.Model.ReferenceNumber;
            var expireTime = callData.Model.ExpireDate.AddHours(7);
            GenerateTopupViewModel resultData = new GenerateTopupViewModel()
            {
                Amount = Amount,
                AccountNumber = await SecureStorage.GetAsync("Account"),
                FirstName = await SecureStorage.GetAsync("AdminName"),
                ReferenceNumber = refNumber,
                ExpireDate = expireTime,
                CheckSum = "",
                LastName = ""
            };
            resultData.CheckSum = Helper.CheckSumTopupCreate(resultData);
            if (callData.IsError == true)
            {
                ErrorViewModel errorView = new ErrorViewModel();
                await PopupNavigation.Instance.PushAsync(new Error(errorView));
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new QRcodePage(resultData));
            }
        }

        private async void Goback()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
