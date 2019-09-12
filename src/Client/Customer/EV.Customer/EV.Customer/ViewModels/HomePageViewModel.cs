using EV.Customer.Interfaces;
using EV.Service.Interfaces;
using EV.Service.Models;
using EV.Service.Services;
using EWalletV2.Api.ViewModels.Transaction;
using EWalletV2.Api.ViewModels.User;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
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
                        PopupNavigation.PushAsync(new Views.TopUpPopUpView(Amount, AdminName, AccountNumber, qrcodeReference));
                    }
                    //Wrong QRCode or QRCode expired
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ICommand ScanToPayCommand { get; set; }
        private async void ScanToPay()
        {
            try
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
                        PopupNavigation.PushAsync(new Views.ScanToPayOne(merchantName, merchantAccountNumber));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

            // รอข้อมูลชื่อนามสกุล จากพี่เสริท
            // AccountNumber , FullName 
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
