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
        public OtpRegisViewModel(string passEmail, string reference)
        {
            _authService = new AuthService();
            title = "การยืนยัน OTP";
            image = "";
            blackDetail = "กรุณาใส่ OTP\nเพื่อยืนยัน email ของคุณ";
            grayDetail = "เราได้ส่ง OTP ไปที่ email ของคุณแล้ว";
            referenceText = "ref. " + reference;
            referenceVisible = true;
            orangeText = "ส่ง OTP อีกครั้ง";
            orangeVisible = true;
            warningText = "";
            warningVisible = false;
            email = passEmail;
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
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public ICommand OrangeTextTab { get; set; }
        public async void SentOtpAgain()
        {
            bool isExistEmail = Unities.CheckEmailFormat(email);
            if (isExistEmail)
            {
                var signInData = await _authService.SignIn(email);
                if (signInData.IsError)
                {
                    
                }
            }
            else
            {
                WarningText = "รูปแบบ Email ไม่ถูกต้อง";
                WarningVisible = true;
            }
        }

        public ICommand InputPin { get; set; }
        public async void CheckForRegister(string value)
        {

        }
        public async void CheckForLogin(string value)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
