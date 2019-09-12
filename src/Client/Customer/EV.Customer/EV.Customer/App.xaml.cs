using EV.Customer.ViewModels;
using EV.Customer.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Customer
{

    public partial class App : Application
    {
        public static string Account { get; set; } = "011111111100";
        public static string Email { get; set; } = "pesor1985@gmail.com";
        public static string FirstName { get; set; } = "คุณ ประเสริฐ";
        public static string LastName { get; set; } = "นามสกุลผมเองครับยยยยยยยยยย......นนนนน";
        public static DateTime BirthDate { get; set; } = DateTime.ParseExact("12/12/2018", "dd/MM/yyyy",null);
        public static string MobileNumber { get; set; } = "0849981919";
        public static EWalletV2.Api.ViewModels.EW_Enumerations.EW_GenderEnum Gender { get; set; } = EWalletV2.Api.ViewModels.EW_Enumerations.EW_GenderEnum.Men;
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Views.TransactionsPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
