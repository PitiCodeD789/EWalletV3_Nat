﻿using EV.Customer.Helper;
using EV.Customer.Views;
using EV.Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class OtpRegisViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly AuthService _authService = new AuthService();
        public OtpRegisViewModel(string passEmail, string passReference, Status.LastPage lastPage)
        {
            title = "การยืนยัน OTP";
            image = "icon_mail";
            blackDetail = "กรุณาใส่ OTP\nเพื่อยืนยัน email ของคุณ";
            grayDetail = "เราได้ส่ง OTP ไปที่ email ของคุณแล้ว";
            referenceText = "ref. " + passReference;
            referenceVisible = true;
            orangeText = "ส่ง OTP อีกครั้ง";
            orangeVisible = true;
            warningText = "";
            warningVisible = false;
            backVisible = true;
            fingerTabVisible = false;
            OrangeTextTab = new Command(SentOtpAgain);
            InputPin = new Command<string>(CheckOtp);
            GoBack = new Command(BackPage);
            email = passEmail;
            checkProcess = lastPage;
            reference = passReference;
            pin = "";
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
        public string BlackDetail
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

        private bool backVisible;
        public bool BackVisible
        {
            get { return backVisible; }
            set { backVisible = value; }
        }

        private bool fingerTabVisible;
        public bool FingerTabVisible
        {
            get { return fingerTabVisible; }
            set { fingerTabVisible = value; }
        }

        private string email;

        private Status.LastPage checkProcess;

        private string reference;

        private string pin;

        public ICommand OrangeTextTab { get; set; }
        public async void SentOtpAgain()
        {
            bool isExistEmail = Unities.CheckEmailFormat(email);
            if (isExistEmail)
            {
                var signInData = await _authService.SignIn(email);
                if (signInData != null && !signInData.IsError)
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
                    await Application.Current.MainPage.Navigation.PushAsync(new PinPage(otpRegis));
                }
                else
                {
                    WarningText = "ไม่สามารถเชื่อมต่อได้";
                    WarningVisible = true;
                    try
                    {
                        Vibration.Vibrate();
                        var duration = TimeSpan.FromSeconds(1);
                        Vibration.Vibrate(duration);
                    }
                    catch (FeatureNotSupportedException ex)
                    {
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            else
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        public ICommand InputPin { get; set; }
        public async void CheckOtp(string value)
        {
            WarningText = "";
            WarningVisible = false;
            if (value == "Delete")
            {
                if (pin.Length > 0)
                {
                    pin = pin.Remove(pin.Length - 1);
                    int countPin = pin.Length;
                    HintColorChange(countPin);
                }
            }
            else
            {
                bool isExistvalue = Unities.CheckDigitaAndLength(value, 1);
                if (!isExistvalue)
                {
                    WarningText = "ค่าที่ใส่ไม่ใช่ตัวเลข";
                    WarningVisible = true;
                    try
                    {
                        Vibration.Vibrate();
                        var duration = TimeSpan.FromSeconds(1);
                        Vibration.Vibrate(duration);
                    }
                    catch (FeatureNotSupportedException ex)
                    {
                    }
                    catch (Exception ex)
                    {
                    }
                }
                pin += value;
                int countPin = pin.Length;
                HintColorChange(countPin);
                if (countPin == 6)
                {
                    var checkOtpData = await _authService.CheckOtp(email, pin, reference);
                    if (checkOtpData != null && !checkOtpData.IsError)
                    {
                        if (checkOtpData.Model != null && checkOtpData.Model.IsValidateOtp)
                        {
                            if (checkProcess == Status.LastPage.Login)
                            {
                                SetPinForAuthViewModel setPinForAuth = new SetPinForAuthViewModel(email);
                                await Application.Current.MainPage.Navigation.PushAsync(new PinPage(setPinForAuth));
                            }
                            else if (checkProcess == Status.LastPage.Register)
                            {
                                await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
                            }
                            else
                            {
                                await Application.Current.MainPage.Navigation.PopToRootAsync();
                            }
                        }
                        else
                        {
                            pin = "";
                            countPin = pin.Length;
                            HintColorChange(countPin);
                            WarningText = "OTP ไม่ถูกต้องหรือหมดอายุ";
                            WarningVisible = true;
                            try
                            {
                                Vibration.Vibrate();
                                var duration = TimeSpan.FromSeconds(1);
                                Vibration.Vibrate(duration);
                            }
                            catch (FeatureNotSupportedException ex)
                            {
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else
                    {
                        pin = "";
                        countPin = pin.Length;
                        HintColorChange(countPin);
                        WarningText = "ไม่สามารถเชื่อมต่อได้";
                        WarningVisible = true;
                        try
                        {
                            Vibration.Vibrate();
                            var duration = TimeSpan.FromSeconds(1);
                            Vibration.Vibrate(duration);
                        }
                        catch (FeatureNotSupportedException ex)
                        {
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                if (countPin > 6)
                {
                    pin = pin.Substring(0, 5);
                }
            }
        }

        private void HintColorChange(int length)
        {
            var hasKeyColor = "Gray";
            var hasNoKeyColor = "White";
            if (length == 0)
            {
                PwHint0 = hasNoKeyColor;
                PwHint1 = hasNoKeyColor;
                PwHint2 = hasNoKeyColor;
                PwHint3 = hasNoKeyColor;
                PwHint4 = hasNoKeyColor;
                PwHint5 = hasNoKeyColor;
            }
            else if (length == 1)
            {
                PwHint0 = hasKeyColor;
                PwHint1 = hasNoKeyColor;
                PwHint2 = hasNoKeyColor;
                PwHint3 = hasNoKeyColor;
                PwHint4 = hasNoKeyColor;
                PwHint5 = hasNoKeyColor;
            }
            else if (length == 2)
            {
                PwHint0 = hasKeyColor;
                PwHint1 = hasKeyColor;
                PwHint2 = hasNoKeyColor;
                PwHint3 = hasNoKeyColor;
                PwHint4 = hasNoKeyColor;
                PwHint5 = hasNoKeyColor;
            }
            else if (length == 3)
            {
                PwHint0 = hasKeyColor;
                PwHint1 = hasKeyColor;
                PwHint2 = hasKeyColor;
                PwHint3 = hasNoKeyColor;
                PwHint4 = hasNoKeyColor;
                PwHint5 = hasNoKeyColor;
            }
            else if (length == 4)
            {
                PwHint0 = hasKeyColor;
                PwHint1 = hasKeyColor;
                PwHint2 = hasKeyColor;
                PwHint3 = hasKeyColor;
                PwHint4 = hasNoKeyColor;
                PwHint5 = hasNoKeyColor;
            }
            else if (length == 5)
            {
                PwHint0 = hasKeyColor;
                PwHint1 = hasKeyColor;
                PwHint2 = hasKeyColor;
                PwHint3 = hasKeyColor;
                PwHint4 = hasKeyColor;
                PwHint5 = hasNoKeyColor;
            }
            else if (length == 6)
            {
                PwHint0 = hasKeyColor;
                PwHint1 = hasKeyColor;
                PwHint2 = hasKeyColor;
                PwHint3 = hasKeyColor;
                PwHint4 = hasKeyColor;
                PwHint5 = hasKeyColor;
            }
        }

        // ------------------------------ Propfull ------------------------------

        private string[] _pwHint = new string[] { "White", "White", "White", "White", "White", "White" };
        public string PwHint0
        {
            get { return _pwHint[0]; }
            set { _pwHint[0] = value; OnPropertyChanged(nameof(PwHint0)); }
        }
        public string PwHint1
        {
            get { return _pwHint[1]; }
            set { _pwHint[1] = value; OnPropertyChanged(nameof(PwHint1)); }
        }
        public string PwHint2
        {
            get { return _pwHint[2]; }
            set { _pwHint[2] = value; OnPropertyChanged(nameof(PwHint2)); }
        }
        public string PwHint3
        {
            get { return _pwHint[3]; }
            set { _pwHint[3] = value; OnPropertyChanged(nameof(PwHint3)); }
        }
        public string PwHint4
        {
            get { return _pwHint[4]; }
            set { _pwHint[4] = value; OnPropertyChanged(nameof(PwHint4)); }
        }
        public string PwHint5
        {
            get { return _pwHint[5]; }
            set { _pwHint[5] = value; OnPropertyChanged(nameof(PwHint5)); }
        }

        public ICommand GoBack { get; set; }
        public async void BackPage()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
