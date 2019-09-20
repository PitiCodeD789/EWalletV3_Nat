﻿using EV.Customer.Interfaces;
using EV.Customer.Views;
using EV.Service.Interfaces;
using EV.Service.Models;
using EV.Service.Services;
using EWalletV2.Api.ViewModels;
using EWalletV2.Api.ViewModels.Transaction;
using EWalletV2.Api.ViewModels.User;
using Newtonsoft.Json;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Customer.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class HomePageViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private readonly ITransactionServices _transactionServices;
        public HomePageViewModel()
        {
            //Initial
            _userService = new UserService();
            _transactionServices = new TransactionServices();
            Email = App.Email;
            Greeting = CheckDatetime();
            FullName = App.FirstName + " " + App.LastName;

            //Command
            ScanToPayCommand = new Command(ScanToPay);
            ScanToTopupCommand = new Command(ScanToTopup);
        }

        private async void ScanToTopup()
        {
            //เปิดกล้อง อ่าน qrcode
            try
            {
                var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();
                if (cameraStatus != PermissionStatus.Granted)
                {
                    cameraStatus = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
                }

                if (cameraStatus == PermissionStatus.Granted)
                {
                    var scanner = DependencyService.Get<IQrScanningService>();
                    var result = await scanner.ScanAsync();

                    if (result != null)
                    {
                        GenerateTopupViewModel QrCodeInfomation = JsonConvert.DeserializeObject<GenerateTopupViewModel>(result);
                        if (CheckSum(QrCodeInfomation))
                        {
                            decimal Amount = QrCodeInfomation.Amount;
                            string AdminName = QrCodeInfomation.FirstName + " " + QrCodeInfomation.LastName;
                            string AccountNumber = QrCodeInfomation.AccountNumber;
                            string qrcodeReference = QrCodeInfomation.ReferenceNumber;
                            await PopupNavigation.PushAsync(new Views.ScanToTopupTwo(Amount, AdminName, AccountNumber, qrcodeReference));
                        }
                        //Wrong QRCode or QRCode expired
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Sorry", "Can not use QR Scanner with out Camera Permission", "Ok");
                }

            }
            catch (Exception ex)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel("QR Code ไม่ถูกต้อง", (int)EW_Enumerations.EW_ErrorTypeEnum.Warning);
                PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
            }
        }
        public ICommand ScanToPayCommand { get; set; }
        private async void ScanToPay()
        {
            try
            {
                var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();
                if (cameraStatus != PermissionStatus.Granted)
                {
                    cameraStatus = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
                }

                if (cameraStatus == PermissionStatus.Granted)
                {

                    var scanner = DependencyService.Get<IQrScanningService>();
                    var result = await scanner.ScanAsync();
                    if (result != null)
                    {
                        GeneratePaymentViewModel QrCodeInfomation = JsonConvert.DeserializeObject<GeneratePaymentViewModel>(result);
                        if (CheckSum(QrCodeInfomation))
                        {
                            string merchantName = QrCodeInfomation.FirstName;
                            string merchantAccountNumber = QrCodeInfomation.AccountNumber;
                            decimal amount = QrCodeInfomation.Amount;
                            string reference = QrCodeInfomation.TransactionReference;
                            PopupNavigation.PushAsync(new Views.ScanToPayOne(merchantName, merchantAccountNumber, amount, reference));
                        }
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Sorry", "Can not use QR Scanner with out Camera Permission", "Ok");
                }
            }
            catch (Exception ex)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel("QR Code ไม่ถูกต้อง", (int)EW_Enumerations.EW_ErrorTypeEnum.Warning);
                PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
            }
        }
        public string Greeting { get; set; }
        private string CheckDatetime()
        {
            int dateTime = DateTime.UtcNow.AddHours(7).Hour;

            string Greeting = "";

            if (dateTime >= 0 && dateTime <= 11)
            {
                Greeting = "สวัสดีตอนเช้า";
            }
            else if (dateTime > 11 && dateTime <= 16)
            {
                Greeting = "สวัสดีตอนกลางวัน";
            }
            else if (dateTime > 16 && dateTime <= 19)
            {
                Greeting = "สวัสดีตอนเย็น";
            }
            else
            {
                Greeting = "สวัสดีตอนค่ำ";
            }
            return Greeting;
        }

        public ICommand ScanToTopupCommand { get; set; }


        private bool CheckSum(object data)
        {
            if (true)
            {
                return true;
            }
            return false;
        }

        public async Task GetTotalBalance()
        {
            ResultServiceModel<AccountViewModel> account = await _userService.GetBalance(Email);
            if (account != null)
            {
                Balance = account.Model.Balance;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        private string _accoutNumber;

        public string AccountNumber
        {
            get { return _accoutNumber; }
            set { _accoutNumber = value; }
        }

        private decimal _balance;
        public decimal Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                NotifyPropertyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        


    }
}
