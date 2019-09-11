using EV.Customer.Helper;
using EV.Service.Interfaces;
using EV.Service.Services;
using EWalletV2.Api.ViewModels;
using EWalletV2.Api.ViewModels.Auth;
using EWalletV2.Api.ViewModels.User;
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
        private readonly IUserService _userService;
        public EditProfilePageViewModel()
        {
            //TODO: CheckGender OnLoad
            //GendenRadioChange(App.Gender());
            _userService = new UserService();
            EditClickCommand = new Command(Edit);
            GendenRadioChangeCommand = new Command(GendenRadioChange);
        }
        public ICommand EditClickCommand { get; set; }
        public ICommand GendenRadioChangeCommand { get; set; }

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

        private async void Edit()
        {
            bool isValidateName = Unities.ValidateName(FirstName);
            bool isValidateLastName = Unities.ValidateName(LastName);
            bool isValidateDate = Unities.ValidateStringDateFormat(BirthDate);
            bool isValidateEmail = Unities.CheckEmailFormat(Email);
            if (isValidateName && isValidateLastName && isValidateDate && isValidateEmail)
            {
                UpdateUserCommand updateUserCommand = new UpdateUserCommand
                {
                    BitrhDate = DateTime.Parse(BirthDate),
                    Email = Email,
                    FirstName = FirstName,
                    LastName = LastName,
                    MobileNumber = MobileNumber,
                    Gender = Gender
                };
                var editResult = await _userService.UpdateUser(updateUserCommand);
                bool isError = editResult.IsError;
                if (isError)
                {
                    await Application.Current.MainPage.DisplayAlert("", "Error", "Ok");
                }
                else
                {

                }
                //TODO EditProfile
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




        private bool _isEditMode;

        public bool IsEditMode
        {
            get { return  _isEditMode; }
            set
            {
                if (value != _isEditMode)
                {
                    _isEditMode = value;
                    OnPropertyChanged();
                }
            }
        }


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


        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(); }
        }

        private string _mobileNumber;
        public string MobileNumber
        {
            get { return _mobileNumber; }
            set { _mobileNumber = value; OnPropertyChanged(); }
        }

        private EW_Enumerations.EW_GenderEnum _gender;
        public EW_Enumerations.EW_GenderEnum Gender
        {
            get { return _gender; }
            set { _gender = value; OnPropertyChanged(); }
        }


        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
