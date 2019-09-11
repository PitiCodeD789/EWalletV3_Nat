using EV.Service.Services;
using Plugin.Fingerprint;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class CustomerSettingViewModel: BaseViewModel
    {
        public CustomerSettingViewModel()
        {
            IsSwitchedToggled = Convert.ToBoolean(SecureStorage.GetAsync("IsFingerprintEnabled").Result);
            NarvigatetoChangePasswordPageCommand = new Command(NarvigatetoChangePasswordPage);
            FingerprintSupported();
        }

        public ICommand NarvigatetoChangePasswordPageCommand { get; set; }


        private bool _isSwitchToggled = true;
        public bool IsSwitchedToggled
        {
            get { return _isSwitchToggled; }
            set
            {
                if (value != _isSwitchToggled)
                {

                    _isSwitchToggled = value;
                    OnPropertyChanged();
                    if (value == true)
                    {
                        FingerCheck();
                    }
                    else
                    {
                        SecureStorage.SetAsync("IsFingerprintEnabled", "false");
                    }
                }
            }
        }

        private bool _isVisibled = true;

        public bool IsVisibled
        {
            get { return _isVisibled; }
            set
            {
                _isVisibled = value;
            }
        }


        private async void FingerprintSupported()
        {
            try
            {
                var result = CrossFingerprint.Current.IsAvailableAsync(true).Result;
                if (!result)
                {
                    IsVisibled = false;
                }
            }
            catch (Exception e)
            {

                IsVisibled = false;
            }

        }


        private async void FingerCheck()
        {
            var result = await CrossFingerprint.Current.IsAvailableAsync(true);
            if (result)
            {
                var auth = await CrossFingerprint.Current.AuthenticateAsync("แสกน Fingerprint");
                if (auth.Authenticated)
                {
                    await SecureStorage.SetAsync("IsFingerprintEnabled", "true");
                }
                else
                {
                    IsSwitchedToggled = false;
                    await SecureStorage.SetAsync("IsFingerprintEnabled", "false");
                }
            }
        }

        private async void NarvigatetoChangePasswordPage()
        {
            //await Application.Current.MainPage.Navigation.PushAsync(new MainPage());
        }
    }
}
