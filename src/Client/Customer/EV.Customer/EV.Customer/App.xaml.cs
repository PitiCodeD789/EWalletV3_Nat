using EV.Customer.ViewModels;
using EV.Customer.Views;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Customer
{

    public partial class App : Application
    {
        public static decimal CustomerBalance { get; set; }
        public static string Account { get; set; }// = "011111111100";
        public static string Email { get; set; }// = "pesor1985@gmail.com";
        public static string FirstName { get; set; }// = "คุณ ประเสริฐ";
        public static string LastName { get; set; }// = "นามสกุลผมเองครับยยยยยยยยยย......นนนนน";
        public static DateTime BirthDate { get; set; }// = DateTime.ParseExact("12/12/2018", "dd/MM/yyyy",null);
        public static string MobileNumber { get; set; }// = "0849981919";
        public static EWalletV2.Api.ViewModels.EW_Enumerations.EW_GenderEnum Gender { get; set; }// = EWalletV2.Api.ViewModels.EW_Enumerations.EW_GenderEnum.Men;
        public App()
        {
            InitializeComponent();
        }

        protected async override void OnStart()
        {
            // Handle when your app starts


            var refreshToken = await SecureStorage.GetAsync("RefreshToken");
            if (refreshToken != null)
            {
                MainPage = new NavigationPage(new PinPage(new LoginByPinViewModel()));
                PermissionReq();

            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
                PermissionReq();

            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private async void PermissionReq()
        {
            try
            {
                var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();
                var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();

                if (cameraStatus != PermissionStatus.Granted)
                {
                    cameraStatus = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
                }
                if (storageStatus != PermissionStatus.Granted)
                {
                    storageStatus = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
                }

                if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
                {
                    await Application.Current.MainPage.DisplayAlert("Sorry", "Can not use EWallet Application with out Camera and Storage Permission", "Ok");
                    Environment.Exit(0);
                }
                
            }
            catch (Exception ex)
            {
               Environment.Exit(0);
            }
        }
    }
}
