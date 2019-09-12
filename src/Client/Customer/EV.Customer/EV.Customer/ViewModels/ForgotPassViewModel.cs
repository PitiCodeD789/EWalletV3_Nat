using EV.Customer.Helper;
using EV.Customer.Views;
using EV.Service.Interfaces;
using EV.Service.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
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
            CancelButton = new Command(ClosePopup);
        }

        public ICommand CancelButton;


        private async void CheckPin()
        {
            DateTime birthDate = new DateTime();
            error = null;
            int errorType = 0;
            try
            {
                var inputDateTime = DateTime.ParseExact(BirthDate,"dd/MM/yyyy", null);
                birthDate = Convert.ToDateTime(inputDateTime);
            }
            catch(Exception e)
            {
                error = "Invalid BirthDate format is\n Example : 25/12/2562";
            }
            bool checkEmailFormat = false;
            if (!string.IsNullOrEmpty(Email))
            {
                checkEmailFormat = Unities.CheckEmailFormat(Email);
            }
            if (!checkEmailFormat)
            {
                error += "\n Invalid Email Format";
            }
            if (!string.IsNullOrEmpty(error))
            {
                ErrorViewModel errorView = new ErrorViewModel(error, errorType);
                await PopupNavigation.Instance.PushAsync(new Error(errorView));
            }
            else
            {
                var resultCaller = await _pinService.CheckForgotPin(birthDate, Email);
                if (resultCaller.IsError)
                {
                    error = "ขออภัย! ไม่สามารถเชื่อมต่อได้";
                    errorType = 1;
                }
                else if (resultCaller == null)
                {
                    error = "ขออภัย! ไม่พบอีเมลล์ที่กรอกในระบบ";
                }
                else
                {
                    string resultRefOtp = resultCaller.Model;
                    await Application.Current.MainPage.Navigation.PushAsync(new PinPage(new OtpForgotPassViewModel(Email,resultRefOtp,birthDate)));
                }
                if (!string.IsNullOrEmpty(error))
                {
                    ErrorViewModel errorView = new ErrorViewModel(error,errorType);
                    await PopupNavigation.Instance.PushAsync(new Error(errorView));

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

        private void ClosePopup()
        {
            PopupNavigation.Instance.PopAllAsync();
        }
    }
}
