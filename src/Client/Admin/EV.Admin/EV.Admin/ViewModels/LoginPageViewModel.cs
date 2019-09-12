using EV.Admin.Views;
using EV.Service.Interfaces;
using EV.Service.Models;
using EV.Service.Services;
using EWalletV2.Api.ViewModels;
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
        private readonly IAuthService _authService;
        public LoginPageViewModel()
        {
            IsProgress = false;
            _authService = new AuthService();
            LoginCommand = new Command(async () => await Login());
        }

        public Command LoginCommand { get; set; }

        async Task Login()
        {
            IsProgress = true;
            try
            {
                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
                {
                    ResultServiceModel<LoginUserAndPassViewModel> loginResult = await _authService.LoginUserAndPass(Username, Password);
                    if (loginResult == null || loginResult.IsError)
                    {
                        ErrorViewModel errorView = new ErrorViewModel("ไม่สามารถเชื่อมต่อกับระบบได้");
                        IsProgress = false;
                        await PopupNavigation.Instance.PushAsync(new Error(errorView));
                    }
                    else
                    {
                        try
                        {
                            var checkRole = int.Parse(loginResult.Model.Account.Substring(0, 2)) == (int)EW_Enumerations.EW_UserTypeEnum.Admin;
                            if (checkRole)
                            {
                                IsProgress = false;
                                var successSave = await StoreValue(loginResult.Model);
                                if (successSave)
                                {
                                    App.Email = Username;
                                    await Application.Current.MainPage.Navigation.PushAsync(new AdminTabbedPage());
                                }

                            }
                            else
                            {
                                ErrorViewModel errorView = new ErrorViewModel("ไม่ได้รับอนุญาติให้เข้าใช้ระบบ", (int)EWalletV2.Api.ViewModels.EW_Enumerations.EW_ErrorTypeEnum.Warning);
                                IsProgress = false;
                                await PopupNavigation.Instance.PushAsync(new Error(errorView));
                            }

                        }
                        catch (Exception e)
                        {
                            ErrorViewModel errorView = new ErrorViewModel("ไม่สามารถเชื่อมต่อกับระบบได้");
                            IsProgress = false;
                            await PopupNavigation.Instance.PushAsync(new Error(errorView));
                        }
                    }
                }
                else
                {
                    ErrorViewModel errorView = new ErrorViewModel("โปรดกรอก Username และ Password", (int)EWalletV2.Api.ViewModels.EW_Enumerations.EW_ErrorTypeEnum.Warning);
                    IsProgress = false;
                    await PopupNavigation.Instance.PushAsync(new Error(errorView));
                }
            }
            catch (Exception e)
            {

            }

        }

        private async Task<bool> StoreValue(LoginUserAndPassViewModel viewModel)
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
                App.AdminName = viewModel.FirstName;
                App.PhoneNumber = viewModel.PhoneNumber;
                return true;
            }
            catch (Exception e)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel("โทรศัพท์ของท่านไม่สามารถใช้งานแอพพลิเคชั่นนี้ได้", (int)EW_Enumerations.EW_ErrorTypeEnum.Warning, CloseApp);
                await PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
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
        private bool _isProgress;

        public bool IsProgress
        {
            get { return _isProgress; }
            set
            {
                _isProgress = value;
                OnPropertyChanged();
            }
        }

    }
}
