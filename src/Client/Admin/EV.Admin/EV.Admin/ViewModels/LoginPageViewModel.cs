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
            await SecureStorage.SetAsync("Account", "0200000005");
            await SecureStorage.SetAsync("Username", "knot@outlook.com");
            await SecureStorage.SetAsync("AdminName", "Knot Agent");
            await SecureStorage.SetAsync("PhoneNumber", "0864479402");
            await SecureStorage.SetAsync("RefreshToken", "ABcD1236");
            await SecureStorage.SetAsync("Token", "12345678952");
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
