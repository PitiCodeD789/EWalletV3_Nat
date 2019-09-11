using EV.Customer.Helper;
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
        private readonly IPinService _pinService;
        string error;
        public ForgotPassViewModel(IPinService pinService)
        {
            _pinService = pinService;
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
            bool checkEmailFormat = Unities.CheckEmailFormat(Email);
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
