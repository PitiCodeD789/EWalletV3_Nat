using EV.Customer.Helper;
using EV.Customer.Views;
using EV.Service.Services;
using EWalletV2.Api.ViewModels;
using EWalletV2.Api.ViewModels.Auth;
using Rg.Plugins.Popup.Services;
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
    public class SetPinForAuthViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly AuthService _authService = new AuthService();
        public SetPinForAuthViewModel(string passEmail)
        {
            DataJoint();
            lastPage = Status.LastPage.Login;
            email = passEmail;
            GoBack = new Command(BackPageByLogin);
        }

        public SetPinForAuthViewModel(RegisterCommand passRegister)
        {
            DataJoint();
            lastPage = Status.LastPage.Register;
            register = passRegister;
            GoBack = new Command(BackPageByRegis);
        }

        public void DataJoint()
        {
            title = "สร้างรหัสผ่าน";
            image = "icon_PIN";
            blackDetail = "สร้างรหัสผ่าน";
            grayDetail = "ใส่รหัสผ่านของคุณ";
            referenceText = "";
            referenceVisible = false;
            orangeText = "";
            orangeVisible = false;
            warningText = "";
            warningVisible = false;
            backVisible = true;
            fingerTabVisible = false;
            pin = "";
            repeatPin = "";
            InputPin = new Command<string>(InputPinMethod);
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

        private Status.LastPage lastPage;

        private string email;

        private string pin;

        private string repeatPin;

        private RegisterCommand register;

        public ICommand OrangeTextTab { get; set; }

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
                    if (lastPage == Status.LastPage.Login)
                    {
                        if (repeatPin == "")
                        {
                            ChangeDataJoint();
                        }
                        else
                        {
                            if (pin == repeatPin)
                            {
                                await Login();
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
                    }
                    else if (lastPage == Status.LastPage.Register)
                    {
                        if (repeatPin == "")
                        {
                            ChangeDataJoint();
                        }
                        else
                        {
                            if (pin == repeatPin)
                            {
                                register.Pin = pin;
                                await Register();
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
                    }
                    else
                    {
                        await Application.Current.MainPage.Navigation.PopToRootAsync();
                    }
                }
                if (countPin > 6)
                {
                    pin = pin.Substring(0, 5);
                }
            }
        }
        public async Task Register()
        {
            var registerData = await _authService.Register(register);
            if (registerData != null && !registerData.IsError)
            {
                if (registerData.Model != null)
                {
                    try
                    {
                        string gender = ((int)register.Gender).ToString();
                        string birthDate = register.BirthDate.ToString("dd/MM/yyyy");
                        await SecureStorage.SetAsync("RefreshToken", registerData.Model.RefreshToken);
                        await SecureStorage.SetAsync("Token", registerData.Model.Token);
                        await SecureStorage.SetAsync("Account", registerData.Model.Account);
                        await SecureStorage.SetAsync("Email", register.Email);
                        await SecureStorage.SetAsync("FirstName", register.FirstName);
                        await SecureStorage.SetAsync("LastName", register.LastName);
                        await SecureStorage.SetAsync("BirthDate", birthDate);
                        await SecureStorage.SetAsync("MobileNumber", register.MobileNumber);
                        await SecureStorage.SetAsync("Gender", gender);
                        App.Account = registerData.Model.Account;
                        App.Email = register.Email;
                        App.FirstName = register.FirstName;
                        App.LastName = register.LastName;
                        App.BirthDate = register.BirthDate;
                        App.MobileNumber = register.MobileNumber;
                        App.Gender = register.Gender;
                        await Application.Current.MainPage.Navigation.PushAsync(new RegistAndFingerSuccess());
                    }
                    catch(Exception e)
                    {
                        ErrorViewModel errorViewModel = new ErrorViewModel("โทรศัพท์ของท่านไม่สามารถใช้งานแอพพลิเคชั่นนี้ได้", (int)EW_Enumerations.EW_ErrorTypeEnum.Warning, CloseApp);
                        await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "ไม่สามารถ Register ได้", "OK");
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                }
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
        public async Task Login()
        {
            var loginData = await _authService.LoginByCustomer(email, pin);
            if (loginData != null && !loginData.IsError)
            {
                if (loginData.Model != null)
                {
                    try
                    {
                        string gender = ((int)loginData.Model.Gender).ToString();
                        string birthDate = loginData.Model.BirthDate.ToString("dd/MM/yyyy");
                        await SecureStorage.SetAsync("RefreshToken", loginData.Model.RefreshToken);
                        await SecureStorage.SetAsync("Token", loginData.Model.Token);
                        await SecureStorage.SetAsync("Account", loginData.Model.Account);
                        await SecureStorage.SetAsync("Email", email);
                        await SecureStorage.SetAsync("FirstName", loginData.Model.FirstName);
                        await SecureStorage.SetAsync("LastName", loginData.Model.LastName);
                        await SecureStorage.SetAsync("BirthDate", birthDate);
                        await SecureStorage.SetAsync("MobileNumber", loginData.Model.MobileNumber);
                        await SecureStorage.SetAsync("Gender", gender);
                        App.Account = loginData.Model.Account;
                        App.Email = email;
                        App.FirstName = loginData.Model.FirstName;
                        App.LastName = loginData.Model.LastName;
                        App.BirthDate = loginData.Model.BirthDate;
                        App.MobileNumber = loginData.Model.MobileNumber;
                        App.Gender = loginData.Model.Gender;
                        await Application.Current.MainPage.Navigation.PushAsync(new RegistAndFingerSuccess());
                    }
                    catch(Exception e)
                    {
                        ErrorViewModel errorViewModel = new ErrorViewModel("โทรศัพท์ของท่านไม่สามารถใช้งานแอพพลิเคชั่นนี้ได้", (int)EW_Enumerations.EW_ErrorTypeEnum.Warning, CloseApp);
                        await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "ไม่สามารถ Register ได้", "OK");
                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                }
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

        public ICommand GoBack { get; set; }

        public async void BackPageByLogin()
        {
            if (repeatPin == "")
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
            else
            {
                SetPinForAuthViewModel setPinForAuth = new SetPinForAuthViewModel(email);
                await Application.Current.MainPage.Navigation.PushAsync(new PinPage(setPinForAuth));
            }
        }

        public async void BackPageByRegis()
        {
            if (repeatPin == "")
            {
                await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
            }
            else
            {
                SetPinForAuthViewModel authViewModel = new SetPinForAuthViewModel(register);
                await Application.Current.MainPage.Navigation.PushAsync(new PinPage(authViewModel));
            }
        }

        public void ChangeDataJoint()
        {
            Title = "ยืนยันรหัสผ่าน";
            BlackDetail = "ยืนยันรหัสผ่าน";
            GrayDetail = "ใส่รหัสผ่านของคุณอีกครั้ง";
            repeatPin = pin;
            pin = "";
            HintColorChange(pin.Length);
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
