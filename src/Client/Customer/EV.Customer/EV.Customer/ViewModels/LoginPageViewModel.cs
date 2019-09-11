using EV.Customer.Helper;
using EV.Service.Interfaces;
using EV.Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        public LoginPageViewModel()
        {
            _authService = new AuthService();
            SignInCommand = new Command(SignIn);
        }

        private async void SignIn()
        {
            bool isEmail = Unities.CheckEmailFormat(Email);
            if(isEmail)
            {
                var result = await _authService.SignIn(Email);
                if (result.IsError || result.Model == null)
                {
                    
                    await Application.Current.MainPage.DisplayAlert("", "Error", "Ok");

                }
                else
                {
                    
                    // TODO: valid or invalid Create viewModels supports Otp page and edit data
                    await Application.Current.MainPage.Navigation.PushAsync(new Page());
                }


            }



        }

        public String Email { get; set; }
        public Command SignInCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
