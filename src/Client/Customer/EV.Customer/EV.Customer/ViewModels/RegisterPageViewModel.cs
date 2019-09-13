using EV.Customer.Helper;
using EV.Customer.Views;
using EV.Service.Interfaces;
using EV.Service.Services;
using EWalletV2.Api.ViewModels;
using EWalletV2.Api.ViewModels.Auth;
using Rg.Plugins.Popup.Services;
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
            Email = App.Email;
        }

        private void GendenRadioChange(object obj)
        {
            if(obj == "Female")
            {
                Gender = EW_Enumerations.EW_GenderEnum.Women;
                BgWomenRadio = "Black";
                BgMenRadio = "White";
            }
            else if(obj == "Male")
            {
                Gender = EW_Enumerations.EW_GenderEnum.Men;
                BgWomenRadio = "White";
                BgMenRadio = "Black";
            }
        }

        private async void Register()
        {
            bool isValidateName = Unities.ValidateName(FirstName);
            if (!isValidateName)
            {
                FirstName = "";
            }
            bool isValidateLastName = Unities.ValidateName(LastName);
            if (!isValidateLastName)
            {
                LastName = "";
            }
            bool isValidateDate = Unities.ValidateStringDateFormat(BirthDate);
            if (!isValidateDate)
            {
                BirthDate = "";
            }
            bool isValidateEmail = Unities.CheckEmailFormat(Email);
            if (!isValidateEmail)
            {
                Email = "";
            }

            bool isValidateMobile = Unities.ValidateStringMobile(MobileNumber);
            if(!isValidateMobile)
            {
                MobileNumber = "";
            }
                
            if (isValidateName && isValidateLastName && isValidateDate && isValidateEmail && isValidateMobile)
            {
                RegisterCommand register = new RegisterCommand
                {
                    BirthDate = DateTime.ParseExact(BirthDate,"dd/MM/yyyy",null),
                    Email = Email,
                    FirstName = FirstName,
                    LastName = LastName,
                    Pin = Pin,
                    MobileNumber = MobileNumber,
                    Gender = Gender

                };
                SetPinForAuthViewModel authViewModel = new SetPinForAuthViewModel(register);
              
                await Application.Current.MainPage.Navigation.PushAsync(new PinPage(authViewModel));

            }else
            {
          
                await PopupNavigation.PushAsync(new Error(new ErrorViewModel("กรอกข้อมูลไม่ถูกต้อง",(int)EW_Enumerations.EW_ErrorTypeEnum.Warning)));
               
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



        private string birthDate ;

        public string BirthDate 
        {
            get { return birthDate; }
            set
            {
                birthDate = Unities.BirthDateSet(value);             
                OnPropertyChanged("BirthDate");
            }
        }


      

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; OnPropertyChanged("FirstName"); }
        }

       

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; OnPropertyChanged("LastName"); }
        }


        private string mobileNumber;

        public string MobileNumber
        {
            get { return mobileNumber; }
            set { mobileNumber = value; OnPropertyChanged("MobileNumber"); }
        }

     
        public EW_Enumerations.EW_GenderEnum Gender { get; set; }
        public string Email { get; set; } = App.Email;
        public string Pin { get; set; }

       




        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
