using Plugin.Fingerprint;
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
    public class FingerprintPageViewModel : INotifyPropertyChanged
    {
        public FingerprintPageViewModel()
        {
            IsShowFingerButton = false;
            FingerprintClickCommand = new Command(FingerprintClick);
            FingerprintAddClickCommand = new Command(FingerprintAddClick);
            IsFingerPrint();
        }


        private async void IsFingerPrint()
        {
            try
            {            
                bool available = await CrossFingerprint.Current.IsAvailableAsync(true);
                IsShowFingerButton = available;
            }
            catch (Exception ex)
            {
                IsShowFingerButton =  false;
            }
          
        }

        private async void FingerprintAddClick(object obj)
        {
            var available = await CrossFingerprint.Current.IsAvailableAsync(true);
            if (available)
            {
                var result = await CrossFingerprint.Current.AuthenticateAsync("Prove you have fingers!");
                if (result.Authenticated)
                {
                    try
                    {
                        await SecureStorage.SetAsync("Active_FingerPrint", "True");
                        IsShowFingerButton = false;
                        IsShowLable = true;
                        OnPropertyChanged("IsShowFingerButton");
                    }
                    catch (Exception ex)
                    {
                        // Possible that device doesn't support secure storage on device.
                    }
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("ok", "Finger not Authenticate", "error");

                }
            }else
            {
                Application.Current.MainPage.DisplayAlert("ok", "not device", "error");
            }
          
        }      

        private void FingerprintClick()
        {
            Application.Current.MainPage.Navigation.PushAsync(new Page());
        }
        private bool isShowFingerButton;

        public bool IsShowFingerButton
        {
            get { return isShowFingerButton; }
            set { isShowFingerButton = value;  }
        }


        private bool isShowLable = false;

        public bool IsShowLable
        {
            get { return isShowLable; }
            set { isShowLable = value; OnPropertyChanged("IsShowLable"); }
        }

   
        public ICommand FingerprintClickCommand { get; set; }
        public ICommand FingerprintAddClickCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
