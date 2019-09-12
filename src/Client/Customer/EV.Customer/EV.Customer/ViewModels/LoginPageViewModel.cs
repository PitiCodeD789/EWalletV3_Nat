using EV.Customer.Helper;
using EV.Customer.Views;
using EV.Service.Interfaces;
using EV.Service.Models;
using EV.Service.Services;
using EWalletV2.Api.ViewModels;
using EWalletV2.Api.ViewModels.Auth;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        public LoginPageViewModel()
        {
            IsProcess = false;
            _authService = new AuthService();
            SignInCommand = new Command(SignIn);
        }

        private async void SignIn()
        {
            IsProcess = true;
            bool isEmail = Unities.CheckEmailFormat(Email);         
            if (isEmail)
            {
                ResultServiceModel<CheckEmailViewModel> result = await _authService.SignIn(Email);
                if (result.IsError || result.Model == null)
                {
                    ErrorViewModel errorView = new ErrorViewModel("ไม่สามารถเชื่อมต่อกับระบบได้");
                    IsProcess = false;
                    await PopupNavigation.Instance.PushAsync(new Error(errorView));
                }
                else
                {
                    App.Email = Email;

                    if (result.Model.IsExist == true)
                    {
                        IsProcess = false;
                        var status = Status.LastPage.Login;
                        await Application.Current.MainPage.Navigation.PushAsync(new PinPage(new OtpRegisViewModel(Email, result.Model.RefNumber, status)));
                    }
                    else
                    {
                        IsProcess = false;
                        var status = Status.LastPage.Register;
                        await Application.Current.MainPage.Navigation.PushAsync(new PinPage(new OtpRegisViewModel(Email, result.Model.RefNumber, status)));
                    }
                }
            }
            else
            {
                ErrorViewModel errorView = new ErrorViewModel("โปรดกรอก Username เป็น format Email", (int)EW_Enumerations.EW_ErrorTypeEnum.Warning);
                IsProcess = false;
                await PopupNavigation.Instance.PushAsync(new Error(errorView));
            }
        }

        private bool isProcess;

        public bool IsProcess
        {
            get { return isProcess; }
            set {
                if (value != isProcess)
                {
                    isProcess = value; OnPropertyChanged();
                }
            }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (value != _email)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }
        public Command SignInCommand { get; set; }
    }
}
