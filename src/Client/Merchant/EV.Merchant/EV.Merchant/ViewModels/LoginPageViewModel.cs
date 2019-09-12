using EV.Merchant.Views;
using EV.Service.Interfaces;
using EV.Service.Models;
using EV.Service.Services;
using EWalletV2.Api.ViewModels;
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
            await StoreValue();
            Application.Current.MainPage = new NavigationPage(new MerchantTabbedPage());
        }

        private async Task StoreValue()
        {
            try
            {
                await SecureStorage.SetAsync("Account", viewModel.Account);
                await SecureStorage.SetAsync("Username", Username);
                await SecureStorage.SetAsync("FirstName", viewModel.FirstName);
                await SecureStorage.SetAsync("PhoneNumber", viewModel.PhoneNumber);
                await SecureStorage.SetAsync("RefreshToken", viewModel.RefreshToken);
                await SecureStorage.SetAsync("Token", viewModel.Token);
                App.Account = viewModel.Account;
                App.Username = Username;
                App.FirstName = viewModel.FirstName;
                App.LastName = viewModel.LastName;
                App.PhoneNumber = viewModel.PhoneNumber;
            }
            catch(Exception e)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel("โทรศัพท์ของท่านไม่สามารถใช้งานแอพพลิเคชั่นนี้ได้", (int)EW_Enumerations.EW_ErrorTypeEnum.Warning, CloseApp);
                await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
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
