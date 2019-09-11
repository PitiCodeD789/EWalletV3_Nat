using EV.Service.Interfaces;
using EV.Service.Models;
using EV.Service.Services;
using EWalletV2.Api.ViewModels.Transaction;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class PaymentPageViewModel
    {
        private readonly ITransactionServices _transactionServices;
        public PaymentPageViewModel()
        {
            //Initial
            _transactionServices = new TransactionServices();

            //Command
            CancelCommand = new Command(Cancel);
            BackToHomeCommand = new Command(BackToHome);
            PaymentCommand = new Command(Payment);
        }

        public ICommand CancelCommand { get; set; }
        private void Cancel()
        {
            PopupNavigation.PopAsync();
        }
        public ICommand BackToHomeCommand { get; set; }
        private void BackToHome()
        {
            //Application.Current.MainPage = new Views.HomePageView();
        }

        public ICommand PaymentCommand { get; set; }
        private async void Payment()
        {
            ResultServiceModel<PaymentViewModel> paymentResult = await _transactionServices.Payment(Email, MerchantAccountNumber, Amount);
            if (!paymentResult.IsError)
            {
                CreateDate = paymentResult.Model.CreateDatetime;
                PopupNavigation.PushAsync(new Views.PaymentSuccessPopUpView(this));
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", "Payment Fail", "Ok");
            }
        }

        private DateTime _createDate;
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }

        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private decimal _customerBalance;
        public decimal CustomerBalance
        {
            get { return _customerBalance; }
            set { _customerBalance = value; }
        }
        private string _merchantName;
        public string MerchantName
        {
            get { return _merchantName; }
            set { _merchantName = value; }
        }
        private string _merchantAccountNumber;
        public string MerchantAccountNumber
        {
            get { return _merchantAccountNumber; }
            set { _merchantAccountNumber = value; }
        }

        public string Email { get; set; }
        public string CustomerAccountNumber { get; set; }

    }
}
