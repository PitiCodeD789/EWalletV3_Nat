using EV.Customer.Helper;
using EV.Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class OtpRegisViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;
        public OtpRegisViewModel(string passEmail, string passReference, Status.LastPage lastPage)
        {
            _authService = new AuthService();
            title = "การยืนยัน OTP";
            image = "";
            blackDetail = "กรุณาใส่ OTP\nเพื่อยืนยัน email ของคุณ";
            grayDetail = "เราได้ส่ง OTP ไปที่ email ของคุณแล้ว";
            referenceText = "ref. " + passReference;
            referenceVisible = true;
            orangeText = "ส่ง OTP อีกครั้ง";
            orangeVisible = true;
            warningText = "";
            warningVisible = false;
            email = passEmail;
            checkProcess = lastPage;
            reference = passReference;
            pin = "";
            OrangeTextTab = new Command(SentOtpAgain);
            InputPin = new Command<string>(CheckOtp);
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        private string blackDetail;
        public string BalckDetail
        {
            get { return blackDetail; }
            set { blackDetail = value; }
        }

        private string grayDetail;
        public string GrayDetail
        {
            get { return grayDetail; }
            set { grayDetail = value; }
        }

        private string referenceText;
        public string ReferenceText
        {
            get { return referenceText; }
            set { referenceText = value; }
        }

        private bool referenceVisible;
        public bool ReferenceVisible
        {
            get { return referenceVisible; }
            set { referenceVisible = value; }
        }

        private string orangeText;
        public string OrangeText
        {
            get { return orangeText; }
            set { orangeText = value; }
        }

        private bool orangeVisible;
        public bool OrangeVisible
        {
            get { return orangeVisible; }
            set { orangeVisible = value; }
        }

        private string warningText;
        public string WarningText
        {
            get
            {
                return warningText;
            }
            set
            {
                warningText = value;
                OnPropertyChanged(nameof(WarningText));
            }
        }

        private bool warningVisible;
        public bool WarningVisible
        {
            get
            {
                return warningVisible;
            }
            set
            {
                warningVisible = value;
                OnPropertyChanged(nameof(WarningVisible));
            }
        }

        private string email;

        private Status.LastPage checkProcess;

        private string reference;

        private string pin;

        public ICommand OrangeTextTab { get; set; }
        //TODO : Input Name Page;
        public async void SentOtpAgain()
        {
            bool isExistEmail = Unities.CheckEmailFormat(email);
            if (isExistEmail)
            {
                var signInData = await _authService.SignIn(email);
                if (signInData.IsError)
                {
                    Status.LastPage lastPage;
                    if (signInData.Model.IsExist)
                    {
                        lastPage = Status.LastPage.Login;
                    }
                    else
                    {
                        lastPage = Status.LastPage.Register;
                    }
                    OtpRegisViewModel otpRegis = new OtpRegisViewModel(email, signInData.Model.RefNumber, lastPage);
                    //await Application.Current.MainPage.Navigation.PushAsync(new Page(otpRegis));
                }
                else
                {
                    WarningText = "ไม่สามารถเชื่อมต่อได้";
                    WarningVisible = true;
                }
            }
            else
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        public ICommand InputPin { get; set; }
        //TODO : Input Name Page;
        //TODO : Waiting Name of next view model
        //TODO : Change Color of 6 Circle;
        public async void CheckOtp(string value)
        {
            bool isExistvalue = Unities.CheckDigitaAndLength(value, 1);
            if (!isExistvalue)
            {
                WarningText = "ค่าที่ใส่ไม่ใช่ตัวเลข";
                WarningVisible = true;
            }
            pin += value;
            int countPin = pin.Length;
            //TODO : Change Color of 6 Circle;
            if(countPin == 6)
            {
                bool isExistEmail = Unities.CheckEmailFormat(email);
                if (isExistEmail)
                {
                    var checkOtpData = await _authService.CheckOtp(email, pin, reference);
                    if (checkOtpData.IsError)
                    {
                        if (checkOtpData.Model.IsValidateOtp)
                        {
                            if(checkProcess == Status.LastPage.Login)
                            {
                                //TODO : Waiting Name of next view model
                                //await Application.Current.MainPage.Navigation.PushAsync();
                            }
                            else if(checkProcess == Status.LastPage.Register)
                            {
                                //TODO : Waiting Name of next view model
                                //await Application.Current.MainPage.Navigation.PushAsync();
                            }
                            else
                            {
                                await Application.Current.MainPage.Navigation.PopToRootAsync();
                            }
                        }
                        else
                        {
                            WarningText = "OTP ไม่ถูกต้องหรือหมดอายุ";
                            WarningVisible = true;
                        }
                    }
                    else
                    {
                        WarningText = "ไม่สามารถเชื่อมต่อได้";
                        WarningVisible = true;
                    }
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                }
            }
        }

        public ICommand GoBack { get; set; }
        public async void BackPage()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
