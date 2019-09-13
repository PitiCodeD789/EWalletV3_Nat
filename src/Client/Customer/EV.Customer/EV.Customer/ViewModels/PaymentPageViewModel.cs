using EV.Customer.Views;
using EV.Service.Interfaces;
using EV.Service.Models;
using EV.Service.Services;
using EWalletV2.Api.ViewModels.Transaction;
using EWalletV2.Api.ViewModels.User;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using static EWalletV2.Api.ViewModels.EW_Enumerations;

namespace EV.Customer.ViewModels
{
    public class PaymentPageViewModel : BaseViewModel
    {
        private readonly ITransactionServices _transactionServices;
        public PaymentPageViewModel()
        {

            GetTotalBalance();
            //Initial
            _transactionServices = new TransactionServices();
            CreateDate = DateTime.Now;
            //Command
            CancelCommand = new Command(Cancel);
            BackToHomeCommand = new Command(BackToHome);
            PaymentCommand = new Command(Payment);
            InputPaymentCommand = new Command(InputPayment);
            ToSlip = new Command(PushToSlip);
            FullName = App.FirstName + " " + App.LastName;
            CustomerAccountNumber = App.Account;
        }

        public ICommand CancelCommand { get; set; }
        public ICommand ToSlip { get; set; }
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
        public ICommand InputPaymentCommand { get; set; }
        private async void Payment()
        {
            try
            {
                IsPaymentEnabled = false;
                ResultServiceModel<PaymentViewModel> paymentResult = await _transactionServices.Payment(Email, MerchantAccountNumber, Amount);
                if (paymentResult == null || paymentResult.IsError) { 
                    //Application.Current.MainPage.DisplayAlert("Error", "Payment Fail", "Ok");
                    ErrorViewModel errorView = new ErrorViewModel("ทำรายการไม่สำเร็จ", (int)EW_ErrorTypeEnum.Error);
                    PopupNavigation.Instance.PushAsync(new Error(errorView));

                }
                else
                
                {
                    CreateDate = paymentResult.Model.CreateDatetime;
                    Reference = paymentResult.Model.Reference;
                    PopupNavigation.Instance.PopAllAsync();
                    PopupNavigation.Instance.PushAsync(new Views.ScanToPayThree(this));
                }
                IsPaymentEnabled = true;
            }
            catch (Exception e)
            {

                throw;
            }
            
        }

        private bool isPaymentEnabled = true;

        public bool IsPaymentEnabled
        {
            get { return isPaymentEnabled; }
            set { isPaymentEnabled = value; OnPropertyChanged(); }
        }

        private async void InputPayment()
        {
            PopupNavigation.Instance.PopAllAsync();
            PopupNavigation.Instance.PushAsync(new ScanToPayTwo(this)); 
        }
        private async void PushToSlip()
        {
            await PopupNavigation.Instance.PopAllAsync();
            SlipViewModel payment = new SlipViewModel()
            {
                Amount = Amount,
                CreateDate = CreateDate,
                OtherAccountNumber = MerchantAccountNumber,
                OtherName = MerchantName,
                Reference = Reference,
                Type = EW_TypeTransectionEnum.Payment
            };
            Application.Current.MainPage.Navigation.PushAsync(new ReceiptPage(payment));
        }

        private DateTime _createDate;
        public DateTime CreateDate
        {
            get { return _createDate; }
            set { _createDate = value; }
        }
        private string fullName;

        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged();
            }
        }

        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        private decimal _customerBalance;
        private decimal customerBalance;

        public decimal CustomerBalance
        {
            get { return customerBalance; }
            set { customerBalance = value;
                OnPropertyChanged();
            }
        }


        public void GetTotalBalance()
        {
            IUserService _userService = new UserService();

            var result = _userService.GetBalance(Email).GetAwaiter();
            result.OnCompleted(() => SetCustomerBalance(result.GetResult().Model.Balance));
        }

        public void SetCustomerBalance(decimal balance)
        {
            CustomerBalance = balance;
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
        public string Reference { get; set; }
        public string Email {  get => App.Email; }

        private string customerAccountNumber;

        public string CustomerAccountNumber
        {
            get { return customerAccountNumber; }
            set { customerAccountNumber = value;
                OnPropertyChanged();
            }
        }


    }
}
