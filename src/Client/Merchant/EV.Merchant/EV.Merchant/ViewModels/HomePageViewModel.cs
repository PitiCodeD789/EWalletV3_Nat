using EV.Merchant.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Merchant.ViewModels
{
    public class HomePageViewModel
    {
        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string TimeString { get; set; }
        public ICommand GotoQRCodePageCommand { get; set; }

        public HomePageViewModel()
        {
            TimeString = CheckDatetime();
            GotoQRCodePageCommand = new Command(GotoQRcodePageAsync);
        }

        private void GotoQRcodePageAsync()
        {
            Application.Current.MainPage.Navigation.PushAsync(new GenerateQRcodePage());
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
