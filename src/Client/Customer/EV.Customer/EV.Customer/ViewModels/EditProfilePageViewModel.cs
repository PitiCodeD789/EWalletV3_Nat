using EV.Customer.Helper;
using EV.Customer.Views;
using EV.Service.Interfaces;
using EV.Service.Services;
using EWalletV2.Api.ViewModels;
using EWalletV2.Api.ViewModels.Auth;
using EWalletV2.Api.ViewModels.User;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class EditProfilePageViewModel :BaseViewModel, INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        public EditProfilePageViewModel()
        {
            //TODO: CheckGender OnLoad
            GendenRadioChange(App.Gender.ToString());
            IsEditMode = false;
            _userService = new UserService();
            EditClickCommand = new Command(Edit);
            GendenRadioChangeCommand = new Command(GendenRadioChange);
            BackPageClickCommand = new Command(BackPageClick);
        }

        private void BackPageClick(object obj)
        {
            PopupNavigation.Instance.PopAsync();
        }

        public ICommand BackPageClickCommand { get; set; }
        public ICommand EditClickCommand { get; set; }
        public ICommand GendenRadioChangeCommand { get; set; }

        private void GendenRadioChange(object obj)
        {
            if ((string)obj == EW_Enumerations.EW_GenderEnum.Women.ToString())
            {
                Gender = EW_Enumerations.EW_GenderEnum.Women;
                BgWomenRadio = "Black";
                BgMenRadio = "White";
            }
            else if ((string)obj == EW_Enumerations.EW_GenderEnum.Men.ToString())
            {
                Gender = EW_Enumerations.EW_GenderEnum.Men;
                BgWomenRadio = "White";
                BgMenRadio = "Black";
            }
        }

        private async void Edit()
        {
            if (IsEditMode)
            {
                bool isValidateName = Unities.ValidateName(FirstName);
                bool isValidateLastName = Unities.ValidateName(LastName);
                bool isValidateDate = Unities.ValidateStringDateFormat(BirthDate);
                bool isValidateEmail = Unities.CheckEmailFormat(Email);
                bool isValidatePhoneNum = Unities.ValidateStringMobile(MobileNumber);
                if (isValidateName && isValidateLastName && isValidateDate && isValidateEmail && isValidatePhoneNum )
                {
                    UpdateUserCommand updateUserCommand = new UpdateUserCommand
                    {
                        BitrhDate = DateTime.ParseExact(BirthDate,"dd/MM/yyyy",null),
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
                        await PopupNavigation.PushAsync(new Error(new ErrorViewModel("Error", (int)EW_Enumerations.EW_ErrorTypeEnum.Warning)));
                       
                    }
                    else
                    {
                        //TODO: Show Completed Popup
                        StoreValue(updateUserCommand);
                        IsEditMode = false;
                        await PopupNavigation.PushAsync(new SavedProfile());

                        
                    }
                }
                else
                {
                        await PopupNavigation.PushAsync(new Error(new ErrorViewModel("กรอกข้อมูลไม่ถูกต้อง", (int)EW_Enumerations.EW_ErrorTypeEnum.Warning)));

                    
                }
            }
            else
            {
                IsEditMode = true;
                ShowEditButton = false;
                
            }

        }

        bool StoreValue(UpdateUserCommand viewModel)
        {
            try
            {
                SecureStorage.SetAsync("BirthDate", viewModel.BitrhDate.ToString());
                SecureStorage.SetAsync("Email", viewModel.Email);
                SecureStorage.SetAsync("FirstName", viewModel.FirstName);
                SecureStorage.SetAsync("Gender", viewModel.Gender.ToString());
                SecureStorage.SetAsync("LastName", viewModel.LastName);
                SecureStorage.SetAsync("MobileNumber", viewModel.MobileNumber);
                App.Email = viewModel.Email;
                App.FirstName = viewModel.FirstName;
                App.Gender = viewModel.Gender;
                App.LastName = viewModel.LastName;
                App.MobileNumber = viewModel.MobileNumber;

                return true;
            }
            catch (Exception e)
            {
                return false;
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




        private bool _isEditMode = false;

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


        private string birthDate = App.BirthDate.ToString("dd/MM/yyyy");

        public string BirthDate
        {
            get { return birthDate; }
            set
            {
                birthDate = Unities.BirthDateSet(value);
                OnPropertyChanged();
            }
        }


        private string _firstName = App.FirstName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(); }
        }

        private string _lastName = App.LastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(); }
        }

        private string _mobileNumber = App.MobileNumber;
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


        private string _email = App.Email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        private bool _showEditButton = true;

        public bool ShowEditButton
        {
            get { return _showEditButton; }
            set { _showEditButton = value; OnPropertyChanged(); }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
