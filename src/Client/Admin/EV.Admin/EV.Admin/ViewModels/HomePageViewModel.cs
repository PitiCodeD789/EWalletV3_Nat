using EV.Admin.Views;
using EV.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EV.Admin.ViewModels
{
    public class HomePageViewModel:BaseViewModel
    {
        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value;
                OnPropertyChanged();
            }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value;
                OnPropertyChanged();
            }
        }
        public string TimeString { get; set; }
        public ICommand GotoQRCodePageCommand { get; set; }

        public HomePageViewModel()
        {
            TimeString = CheckDatetime();
            FullName = SecureStorage.GetAsync("AdminName").Result;
            GotoQRCodePageCommand = new Command(GotoQRcodePageAsync);
        }

        private void GotoQRcodePageAsync()
        {
            Application.Current.MainPage.Navigation.PushAsync(new TopupPage());
        }
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
    }
}
