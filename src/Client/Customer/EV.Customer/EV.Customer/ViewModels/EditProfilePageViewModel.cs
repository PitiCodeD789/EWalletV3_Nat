using EV.Customer.Helper;
using EV.Service.Interfaces;
using EV.Service.Services;
using EWalletV2.Api.ViewModels;
using EWalletV2.Api.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class EditProfilePageViewModel : INotifyPropertyChanged
    {
        private readonly IAuthService _authService;
        public EditProfilePageViewModel()
        {
            //TODO: CheckGender OnLoad
            //GendenRadioChange(App.Gender());
            _authService = new AuthService();
            RegisterClickCommand = new Command(Register);
            GendenRadioChangeCommand = new Command(GendenRadioChange);
        }


        private void GendenRadioChange(object obj)
        {
            if ((string)obj == "F")
            {
                Gender = EW_Enumerations.EW_GenderEnum.Women;
                BgWomenRadio = "Black";
                BgMenRadio = "White";
            }
            else if ((string)obj == "M")
            {
                Gender = EW_Enumerations.EW_GenderEnum.Men;
                BgWomenRadio = "White";
                BgMenRadio = "Black";
            }
        }

        private async void Register()
        {
            bool isValidateName = Unities.ValidateName(FirstName);
            bool isValidateLastName = Unities.ValidateName(LastName);
            bool isValidateDate = Unities.ValidateStringDateFormat(BirthDate);
            bool isValidateEmail = Unities.CheckEmailFormat(Email);
            if (isValidateName && isValidateLastName && isValidateDate && isValidateEmail)
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
                await Application.Current.MainPage.Navigation.PushAsync(new Page());

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", "Error", "Ok");
            }


        }



        private string bgWomenRadio;

        public string BgWomenRadio
        {
            get { return bgWomenRadio; }
            set { bgWomenRadio = value; OnPropertyChanged(); }
        }


        private string bgMenRadio;

        public string BgMenRadio
        {
            get { return bgMenRadio; }
            set { bgMenRadio = value; OnPropertyChanged(); }
        }


        public ICommand RegisterClickCommand { get; set; }
        public ICommand GendenRadioChangeCommand { get; set; }



        private string birthDate;

        public string BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = Unities.BirthDateSet(value);
                OnPropertyChanged();
            }
        }


        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string MobileNumber { get; set; }
        public EW_Enumerations.EW_GenderEnum Gender { get; set; }
        public string Email { get; set; }
        public string Pin { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
