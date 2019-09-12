using EV.Merchant.Views;
using EV.Service.Interfaces;
using EV.Service.Models;
using EV.Service.Services;
using EWalletV2.Api.ViewModels.Auth;
using Rg.Plugins.Popup.Services;
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
    public class LoginPageViewModel : BaseViewModel
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
                    ErrorViewModel errorView = new ErrorViewModel("ไม่สามารถเชื่อมต่อกับระบบได้");
                    await PopupNavigation.Instance.PushAsync(new Error(errorView));
                }
                else
                {
                    await StoreValue(loginResult.Model);
                    await Application.Current.MainPage.Navigation.PushAsync(new MerchantTabbedPage());
                }
            }
            else
            {
                ErrorViewModel errorView = new ErrorViewModel("โปรดกรอก Username และ Password");
                await PopupNavigation.Instance.PushAsync(new Error(errorView));
            }
        }

        private async Task StoreValue(LoginUserAndPassViewModel viewModel)
        {
            try
            {
                await SecureStorage.SetAsync("Account", viewModel.Account);
                await SecureStorage.SetAsync("Username", Username);
                await SecureStorage.SetAsync("FirstName", viewModel.FirstName);
                await SecureStorage.SetAsync("PhoneNumber", viewModel.PhoneNumber);
                await SecureStorage.SetAsync("RefreshToken", viewModel.RefreshToken);
                await SecureStorage.SetAsync("Token", viewModel.Token);
                App.RefreshToken = viewModel.RefreshToken;
                App.Token = viewModel.Token;
                App.Account = viewModel.Account;
                App.Username = Username;
                App.FirstName = viewModel.FirstName;
                App.LastName = viewModel.LastName;
                App.PhoneNumber = viewModel.PhoneNumber;
            }
            catch(Exception e)
            {
                App.RefreshToken = viewModel.RefreshToken;
                App.Token = viewModel.Token;
                App.Account = viewModel.Account;
                App.Username = Username;
                App.FirstName = viewModel.FirstName;
                App.LastName = viewModel.LastName;
                App.PhoneNumber = viewModel.PhoneNumber;
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

    }
}
