using EV.Service.Interfaces;
using EV.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class ProfilePageViewModel
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public ProfilePageViewModel()
        {
            //Initial
            _userService = new UserService();
            _authService = new AuthService();

            //Command
            ViewProfileCommand = new Command(ViewProfile);
            SettingCommand = new Command(SettingAccount);
            LogoutCommand = new Command(Logout);
        }
        

        public ICommand ViewProfileCommand { get; set; }
        public async void ViewProfile()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.EditProfilePage());
        }
        public ICommand SettingCommand { get; set; }
        public void SettingAccount()
        {
            //ไปหน้าตั้งค่า
        }
        public ICommand LogoutCommand { get; set; }
        public void Logout()
        {
            _authService.Logout(Email);
            //กลับไปหน้าแรก
           // Application.Current.MainPage = new MainPage();
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        private string _telNumber;
        public string TelNumber
        {
            get { return _telNumber; }
            set { _telNumber = value; }
        }




    }
}
