using EV.Customer.Helper;
using EV.Customer.Views;
using EV.Service.Services;
using EWalletV2.Api.ViewModels;
using Plugin.Fingerprint;
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
    public class LoginByPinViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly PinService _pinService = new PinService();
        private readonly AuthService _authService = new AuthService();
        public LoginByPinViewModel()
        {
            title = "";
            image = "icon_PIN";
            blackDetail = "ใส่รหัสผ่าน";
            grayDetail = "ใส่รหัสผ่านของคุณ";
            referenceText = "";
            referenceVisible = false;
            orangeText = "ลืมรหัสผ่าน";
            orangeVisible = true;
            warningText = "";
            warningVisible = false;
            backVisible = false;
            pin = "";
            
            OrangeTextTab = new Command(GoToForgotPasswordPage);
            InputPin = new Command<string>(LoginByPin);
            
            try
            {
                email = SecureStorage.GetAsync("Email").Result;
                bool checkFingerPrint = Convert.ToBoolean(SecureStorage.GetAsync("IsFingerprintEnabled").Result);
                if (checkFingerPrint)
                {
                    fingerTabVisible = true;
                    Fingerprint = new Command(LoginByFingerprint);
                }
                else
                {
                    fingerTabVisible = false;
                }
                bool isExistEmail = Unities.CheckEmailFormat(email);
                if (!isExistEmail)
                {
                    ForceLogout();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Get Email And Fingerprint in LoginViewModel : {0}",e);
                ForceLogout();
            }
            try
            {
                countLogin = Int32.Parse(SecureStorage.GetAsync("CountLogin").Result);
            }
            catch (Exception e)
            {
                countLogin = 0;
                Console.WriteLine("countLogin in LoginViewModel : {0}", e);
            }

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

        private string pin;

        private int countLogin;

        public ICommand Fingerprint { get; set; }
        public async void LoginByFingerprint()
        {
            var result = await CrossFingerprint.Current.AuthenticateAsync("Prove you have fingers!");
            if (result.Authenticated)
            {
                try
                {
                    var refreshToken = await SecureStorage.GetAsync("RefreshToken"); ;
                    var tokenData = await _authService.GetTokenByRefreshToken(email, refreshToken);
                    if (tokenData != null || !tokenData.IsError)
                    {
                        if (tokenData.Model == null || tokenData.Model.Token == null)
                        {
                            ErrorViewModel errorViewModel = new ErrorViewModel("กรุณาเข้าสู่ระบบอีกครั้ง", (int)EW_Enumerations.EW_ErrorTypeEnum.Error, ForceLogoutForErrorPopup);
                            await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                        }
                        await SecureStorage.SetAsync("Token", tokenData.Model.Token);
                        App.Account = await SecureStorage.GetAsync("Account");
                        App.Email = await SecureStorage.GetAsync("Email");
                        App.FirstName = await SecureStorage.GetAsync("FirstName");
                        App.LastName = await SecureStorage.GetAsync("LastName");
                        string preBirthDate = await SecureStorage.GetAsync("BirthDate");
                        try
                        {
                            App.BirthDate = DateTime.Parse(preBirthDate);
                        }
                        catch (Exception)
                        {

                        }
                        App.MobileNumber = await SecureStorage.GetAsync("MobileNumber");
                        int gender = 0;
                        int.TryParse(await SecureStorage.GetAsync("Gender"), out gender);
                        App.Gender = (EWalletV2.Api.ViewModels.EW_Enumerations.EW_GenderEnum)gender;
                        Application.Current.MainPage = new NavigationPage(new UserTabbedPage());
                    }
                    else
                    {
                        pin = "";
                        int countPin = pin.Length;
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
                catch (Exception e)
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel("โทรศัพท์ของท่านไม่สามารถใช้งานแอพพลิเคชั่นนี้ได้", (int)EW_Enumerations.EW_ErrorTypeEnum.Warning, CloseApp);
                    await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                }
            }
            else
            {
                ErrorViewModel errorViewModel = new ErrorViewModel("ลายนิ้วมือไม่ถูกต้อง", (int)EW_Enumerations.EW_ErrorTypeEnum.Warning);
                await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
            }
        }


        public ICommand OrangeTextTab { get; set; }
        public async void GoToForgotPasswordPage()
        {
            await PopupNavigation.Instance.PushAsync(new ForgotPassword());
    
        }

        public ICommand InputPin { get; set; }
        public async void LoginByPin(string value)
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
                    var loginPinData = await _pinService.LoginByPin(pin, email);

                    if (loginPinData != null && !loginPinData.IsError && loginPinData.Model != null)
                    {
                        if (loginPinData.Model.IsLogin)
                        {
                            try
                            {
                                await SecureStorage.SetAsync("CountLogin", "0");
                                var refreshToken = await SecureStorage.GetAsync("RefreshToken"); ;
                                var tokenData = await _authService.GetTokenByRefreshToken(email, refreshToken);
                                if (tokenData != null || !tokenData.IsError)
                                {
                                    if (tokenData.Model == null || tokenData.Model.Token == null)
                                    {
                                        ErrorViewModel errorViewModel = new ErrorViewModel("กรุณาเข้าสู่ระบบอีกครั้ง", (int)EW_Enumerations.EW_ErrorTypeEnum.Error, ForceLogoutForErrorPopup);
                                        await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                                    }
                                    await SecureStorage.SetAsync("Token", tokenData.Model.Token);
                                    App.Account = await SecureStorage.GetAsync("Account");
                                    App.Email = await SecureStorage.GetAsync("Email");
                                    App.FirstName = await SecureStorage.GetAsync("FirstName");
                                    App.LastName = await SecureStorage.GetAsync("LastName");
                                    string preBirthDate = await SecureStorage.GetAsync("BirthDate");
                                    try
                                    {
                                        App.BirthDate = DateTime.Parse(preBirthDate);
                                    }
                                    catch (Exception)
                                    {

                                    }
                                    App.MobileNumber = await SecureStorage.GetAsync("MobileNumber");
                                    int gender = 0;
                                    int.TryParse(await SecureStorage.GetAsync("Gender"), out gender);
                                    App.Gender = (EWalletV2.Api.ViewModels.EW_Enumerations.EW_GenderEnum)gender;
                                    Application.Current.MainPage = new NavigationPage(new UserTabbedPage());
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
                            catch (Exception e)
                            {
                                ErrorViewModel errorViewModel = new ErrorViewModel("โทรศัพท์ของท่านไม่สามารถใช้งานแอพพลิเคชั่นนี้ได้", (int)EW_Enumerations.EW_ErrorTypeEnum.Warning, CloseApp);
                                await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
                            }
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
                if (countPin > 6)
                {
                    pin = pin.Substring(0, 5);
                    HintColorChange(pin.Length);
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
    }
}
