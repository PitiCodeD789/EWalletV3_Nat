using EV.Service.Interfaces;
using EV.Service.Services;
using EWalletV2.Api.ViewModels;
using EWalletV2.Api.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class RegisterPageViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        public RegisterPageViewModel()
        {
            _authService = new AuthService();
            RegisterClickCommand = new Command(Register);
            GendenRadioChangeCommand = new Command(GendenRadioChange);
        }

        private void GendenRadioChange(object obj)
        {
            if(obj == "F")
            {
                Gender = EW_Enumerations.EW_GenderEnum.Women;
                BgWomenRadio = "Black";
                BgMenRadio = "White";
            }
            else if(obj == "M")
            {
                Gender = EW_Enumerations.EW_GenderEnum.Men;
                BgWomenRadio = "White";
                BgMenRadio = "Black";
            }
        }

        private async void Register()
        {

            RegisterCommand register = new RegisterCommand
            {
                BirthDate = DateTime.Parse(BirthDate),
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Pin = Pin,
                MobileNumber = MobileNumber,
                Gender = Gender

            };
            var result = await _authService.Register(register);
            if(result.IsError)
            {
                await Application.Current.MainPage.DisplayAlert("", "Error", "Ok");
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new Page());
            }
                
        }

       

        private string bgWomenRadio = "Black";

        public string BgWomenRadio
        {
            get { return bgWomenRadio; }
            set { bgWomenRadio = value; OnPropertyChanged("BgWomenRadio"); }
        }


        private string bgMenRadio = "White";

        public string BgMenRadio
        {
            get { return bgMenRadio; }
            set { bgMenRadio = value; OnPropertyChanged("BgMenRadio"); }
        }


        public ICommand RegisterClickCommand { get; set; }
        public ICommand GendenRadioChangeCommand { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string MobileNumber { get; set; }
        public EW_Enumerations.EW_GenderEnum Gender { get; set; }
        public string Email { get; set; }
        public string Pin { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
