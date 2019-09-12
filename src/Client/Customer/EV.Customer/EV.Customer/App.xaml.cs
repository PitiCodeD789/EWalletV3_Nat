using EV.Customer.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Customer
{
    public partial class App : Application
    {
        public static decimal CustomerBalance { get; set; } = 99999999m;
        public static string FirstName { get; set; } = "TestKub";
        public static string LastName { get; set; } = "TestKub";
        public static string AccountNumber { get; set; } = "456789456"; 
        public static string Email { get; set; } = "Test@Test.test";

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            //MainPage = new NavigationPage(new ForgotPassword());
            MainPage = new NavigationPage(new TransactionsPage());
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
