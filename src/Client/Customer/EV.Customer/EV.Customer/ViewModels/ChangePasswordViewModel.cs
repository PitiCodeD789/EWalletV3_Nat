using EV.Customer.Helper;
using EV.Customer.Views;
using EV.Service.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class ChangePasswordViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly PinService _pinService = new PinService();
        public ChangePasswordViewModel()
        {
            title = "เปลี่ยนรหัสผ่าน";
            image = "icon_PIN";
            blackDetail = "ใส่รหัสผ่าน";
            grayDetail = "ใส่รหัสผ่านของคุณ";
            referenceText = "";
            referenceVisible = false;
            orangeText = "ลืมรหัสผ่าน";
            orangeVisible = true;
            warningText = "";
            warningVisible = false;
            backVisible = true;
            fingerTabVisible = false;
            OrangeTextTab = new Command(GoToForgotPasswordPage);
            InputPin = new Command<string>(InputPinMethod);
            GoBack = new Command(BackPage);
            pin = "";
            oldPin = "";
            newPin = "";
            repeatNewPin = "";
            email = App.Email;
            bool isExistEmail = Unities.CheckEmailFormat(email);
            if (!isExistEmail)
            {
                ForceLogout();
            }
            try
            {
                countLogin = Int32.Parse(SecureStorage.GetAsync("CountLogin").Result);
            }
            catch (Exception e)
            {
                countLogin = 0;
                Console.WriteLine("countLogin in ChangePasswordViewModel : {0}", e);
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
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
            get
            {
                return blackDetail;
            }
            set
            {
                blackDetail = value;
                OnPropertyChanged(nameof(BlackDetail));
            }
        }

        private string grayDetail;
        public string GrayDetail
        {
            get
            {
                return grayDetail;
            }
            set
            {
                grayDetail = value;
                OnPropertyChanged(nameof(GrayDetail));
            }
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

        private string pin;

        private string oldPin;

        private string newPin;

        private string repeatNewPin;

        private int countLogin;

        public ICommand OrangeTextTab { get; set; }
        public async void GoToForgotPasswordPage()
        {
            await PopupNavigation.Instance.PushAsync(new ForgotPassword());
            
        }

        public ICommand InputPin { get; set; }
        public async void InputPinMethod(string value)
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
                    if(oldPin != "" && newPin != "")
                    {
                        repeatNewPin = pin;
                        if(newPin == repeatNewPin)
                        {
                            var updateData = await _pinService.UpdatePin(newPin, oldPin, email);
                            if(updateData != null && !updateData.IsError)
                            {

                                await PopupNavigation.Instance.PushAsync(new SavedProfile());
                                await Application.Current.MainPage.Navigation.PopAsync();
                            }
                        }
                        else
                        {
                            pin = "";
                            countPin = pin.Length;
                            HintColorChange(countPin);
                            WarningText = "รหัสผ่านทั้ง 2 ครั้งไม่ตรงกัน";
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
                    else if(oldPin != "")
                    {
                        ChangeDataRepeatNewPin();
                    }
                    else
                    {
                        var loginPinData = await _pinService.LoginByPin(pin, email);

                        if (loginPinData != null && !loginPinData.IsError && loginPinData.Model != null)
                        {
                            if (loginPinData.Model.IsLogin)
                            {
                                await SecureStorage.SetAsync("CountLogin", "0");
                                ChangeDataNewPin();
                            }
                            else
                            {
                                pin = "";
                                countPin = pin.Length;
                                HintColorChange(countPin);
                                WarningText = "รหัสผ่านไม่ถูกต้อง";
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
                                countLogin++;
                                await SecureStorage.SetAsync("CountLogin", countLogin.ToString());
                                if (countLogin >= 5)
                                {
                                    ForceLogout();
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
                }
                if (countPin > 6)
                {
                    pin = pin.Substring(0, 5);
                }
            }
        }

        public ICommand GoBack { get; set; }
        public async void BackPage()
        {
            if (oldPin == "" && newPin == "")
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else if (newPin == "")
            {
                BlackDetail = "ใส่รหัสผ่าน";
                GrayDetail = "ใส่รหัสผ่านของคุณ";
                oldPin = "";
                newPin = "";
                repeatNewPin = "";
                pin = "";
                int countPin = pin.Length;
                HintColorChange(countPin);
            }
            else
            {
                repeatNewPin = "";
                ChangeDataNewPin();
            }
        }

        public void ChangeDataNewPin()
        {
            BlackDetail = "สร้างรหัสผ่าน";
            GrayDetail = "ใส่รหัสผ่านของคุณ";
            oldPin = pin;
            pin = "";
            int countPin = pin.Length;
            HintColorChange(countPin);
        }

        public void ChangeDataRepeatNewPin()
        {
            BlackDetail = "ยืนยันรหัสผ่าน";
            GrayDetail = "ใส่รหัสผ่านของคุณอีกครั้ง";
            newPin = pin;
            pin = "";
            int countPin = pin.Length;
            HintColorChange(countPin);
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
    }
}
