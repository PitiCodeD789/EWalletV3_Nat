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
        private bool isProgress;

        public bool IsProgress
        {
            get { return isProgress; }
            set
            {
                isProgress = value;
                OnPropertyChanged();
            }
        }

        public InputMoneyViewModel()
        {
            IsProgress = false;
            _transactionService = new TransactionServices();
            BacktoPreviousCommand = new Command(Goback);
            GenerateQRcodeCommand = new Command(GenerateQrcode);
        }
      


        private async void GenerateQrcode()
        {
            IsProgress = true;
            if (Amount < 1)
            {
                ErrorViewModel errorView = new ErrorViewModel("จำนวนเงินไม่สามารถติดลบ หรือ เท่ากับ 0 ได้", (int)EWalletV2.Api.ViewModels.EW_Enumerations.EW_ErrorTypeEnum.Warning);
                await PopupNavigation.Instance.PushAsync(new Error(errorView));
                IsProgress = false;
            }
            else
            {

            var accountNumber = App.Account;
            var callData = await _transactionService.GenerateTopup(accountNumber, Amount);
            var refNumber = callData.Model.ReferenceNumber;
            var expireTime = callData.Model.ExpireDate.AddHours(7);
            GenerateTopupViewModel resultData = new GenerateTopupViewModel()
            {
                Amount = Amount,
                AccountNumber = App.Account,
                FirstName = App.AdminName,
                ReferenceNumber = refNumber,
                ExpireDate = expireTime,
                CheckSum = "",
                LastName = ""
            };
            resultData.CheckSum = Helper.CheckSumTopupCreate(resultData);
            if (callData.IsError == true)
            {
                IsProgress = false;
                ErrorViewModel errorView = new ErrorViewModel();
                await PopupNavigation.Instance.PushAsync(new Error(errorView));
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new QRcodePage(resultData));
                IsProgress = false;
            }
            }
        }

        private async void Goback()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
