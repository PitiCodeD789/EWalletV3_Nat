using EV.Admin.Views;
using EV.Service.Interfaces;
using EV.Service.Models;
using EV.Service.Services;
using EWalletV2.Api.ViewModels.Auth;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

namespace EV.Admin.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public LoginPageViewModel()
        {
            LoginCommand = new Command(async () => await Login());
        }

        public Command LoginCommand { get; set; }

        async Task Login()
        {
            await StoreValue();
            Application.Current.MainPage = new NavigationPage(new AdminTabbedPage());
            
        }

        private async Task StoreValue()
        {
            try
            {
                await SecureStorage.SetAsync("Account", viewModel.Account);
                await SecureStorage.SetAsync("Username", Username);
                await SecureStorage.SetAsync("FirstName", viewModel.FirstName);
                await SecureStorage.SetAsync("LastName", viewModel.LastName);
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
