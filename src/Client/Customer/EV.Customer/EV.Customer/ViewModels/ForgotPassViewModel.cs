using EV.Customer.Helper;
using EV.Customer.Views;
using EV.Service.Interfaces;
using EV.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class ForgotPassViewModel : BaseViewModel
    {
        private readonly IPinService _pinService = new PinService();
        string error;
        int beforeLength;

        public ForgotPassViewModel( )
        {
            CallCheckForgotPin = new Command(execute: CheckPin);
        }
        private async void CheckPin()
        {
            DateTime birthDate = new DateTime();          
            try
            {
                var inputDateTime = DateTime.ParseExact(BirthDate,"dd/MM/yyyy", null);
                birthDate = Convert.ToDateTime(inputDateTime);
            }
            catch(Exception e)
            {
                error =  "Please input datime follow format : dd/MM/yyyy";
            }
            bool checkEmailFormat = false;
            if (!string.IsNullOrEmpty(Email))
            {
                checkEmailFormat = Unities.CheckEmailFormat(Email);
            }
            if (!checkEmailFormat)
            {
                error += "\nYour input format email is incorrect";
            }
            if (!string.IsNullOrEmpty(error))
            {
                await Application.Current.MainPage.DisplayAlert("", error, "OK");
            }
            else
            {
                var resultCaller = _pinService.CheckForgotPin(birthDate, Email).Result;
                if (resultCaller.IsError)
                {
                    error = "ขออภัย! ไม่สามารถเชื่อมต่อได้";
                }
                else if (resultCaller == null)
                {
                    error = "ขออภัย! ไม่พบอีเมลล์ที่กรอกในระบบ";
                }
                else
                {
                    string resultRefOtp = resultCaller.Model;
                    await Application.Current.MainPage.Navigation.PushAsync(new PinPage(new OtpForgotPassViewModel(Email,resultRefOtp,birthDate)));
                    ////////////// ===========> Next page will show refOtp
                }
                if (!string.IsNullOrEmpty(error))
                {
                    await Application.Current.MainPage.DisplayAlert("", error, "OK");
                }
            }
        }
        private string _birthDate;

        public string BirthDate
        {
            get { return _birthDate; }
            set {
                if (value != _birthDate)
                {
                    _birthDate = value;
                    OnPropertyChanged();

                    if ((BirthDate.Length == 2 && beforeLength < 2) || BirthDate.Length == 3 && 
                        beforeLength == 2 && BirthDate.Substring(2) != "/")
                    {
                        BirthDate = BirthDate.Insert(2, "/");
                    }
                    if ((BirthDate.Length == 5 && beforeLength < 5) || BirthDate.Length == 6 && 
                        beforeLength == 5 && BirthDate.Substring(5) != "/")
                    {
                        BirthDate = BirthDate.Insert(5, "/");
                    }

                    beforeLength = BirthDate.Length;

                }
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set {
                if (value != _email)
                {
                    _email = value; OnPropertyChanged();
                }
            }
        }

        public Command CallCheckForgotPin { get; }
    }
}
