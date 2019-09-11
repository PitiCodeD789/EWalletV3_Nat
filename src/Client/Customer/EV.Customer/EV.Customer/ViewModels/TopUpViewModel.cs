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
    public class TopUpViewModel
    {
        private readonly ITransactionServices _transactionServices;

        public TopUpViewModel()
        {
            //Initial
            _transactionServices = new TransactionServices();


            //Command
            CancelCommand = new Command(Cancel);
            BackToHomeCommand = new Command(BackToHome);
            TopUpCommand = new Command(TopUp);
        }

        public void GetDetail()
        {
            //เอาอีเมลมาจาก Class static
            Email = "mock@mock.mock";
            CustomerAccountNumber = "9999999";
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
        public ICommand TopUpCommand { get; set; }
        private async void TopUp()
        {
            ResultServiceModel<TopupViewModel> TopUpResult = await _transactionServices.Topup(Email, QRCodeReference);
            if (!TopUpResult.IsError || TopUpResult.Model.IsSuccess)
            {
                PopupNavigation.PushAsync(new Views.TopUpSuccessPopUpView(this));
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", "TopUp Fail", "Ok");
            }
        }


        //รอแก้ให้ API รีเทรินกลับมาให้
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
        private string _adminName;
        public string AdminName
        {
            get { return _adminName; }
            set { _adminName = value; }
        }
        private string _adminAccountNumber;
        public string AdminAccountNumber
        {
            get { return _adminAccountNumber; }
            set { _adminAccountNumber = value; }
        }
        private string _qrcodeReference;
        public string QRCodeReference
        {
            get { return _qrcodeReference; }
            set { _qrcodeReference = value; }
        }

        public string Email { get; set; }
        public string CustomerAccountNumber { get; set; }



    }
}
