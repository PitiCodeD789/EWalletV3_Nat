using EV.Service.Interfaces;
using EV.Service.Models;
using EV.Service.Services;
using EWalletV2.Api.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EV.Merchant.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        public LoginPageViewModel()
        {
            _authService = new AuthService();
            LoginCommand = new Command(async () => await Login());
        }

        public Command LoginCommand { get; set; }

        async Task Login()
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                ResultServiceModel<LoginUserAndPassViewModel> loginResult = await _authService.LoginUserAndPass(Username, Password);
                if (!loginResult.IsError)
                {
                    //TODO: Edit Alert
                    await Application.Current.MainPage.DisplayAlert("", "Error Login", "Ok");
                }
                else
                {
                    bool storeResult = StoreValue(loginResult.Model);
                    if (!storeResult)
                    {
                        //TODO: Edit Alert
                        await Application.Current.MainPage.DisplayAlert("", "Error Saving Secure Storage", "Ok");
                    }
                    else
                    {
                        //Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                    }
                }
            }
            else
            {
                //TODO: Edit Alert
                await Application.Current.MainPage.DisplayAlert("", "Enter Username and Password", "Ok");
            }
        }

        bool StoreValue(LoginUserAndPassViewModel viewModel)
        {
            try
            {
                SecureStorage.SetAsync("Account", viewModel.Account);
                SecureStorage.SetAsync("FirstName", viewModel.FirstName);
                SecureStorage.SetAsync("LastName", viewModel.LastName);
                SecureStorage.SetAsync("PhoneNumber", viewModel.PhoneNumber);
                SecureStorage.SetAsync("RefreshToken", viewModel.RefreshToken);
                SecureStorage.SetAsync("Token", viewModel.Token);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (Regex.IsMatch(value, "^[\\w\\.@]{0,100}$"))
                {
                    _username = value;
                }
                OnPropertyChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (Regex.IsMatch(value, "^[\\w\\.@]{0,100}$"))
                {
                    _password = value;
                }
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
