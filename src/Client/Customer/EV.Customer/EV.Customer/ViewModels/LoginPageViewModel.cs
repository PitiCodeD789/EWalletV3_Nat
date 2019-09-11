using EV.Service.Interfaces;
using EV.Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace EV.Customer.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        public LoginPageViewModel()
        {
            _authService = new AuthService();
        }

        public String Email { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
