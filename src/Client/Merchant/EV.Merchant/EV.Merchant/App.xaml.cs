using EV.Merchant.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Merchant
{
    public partial class App : Application
    {
        public static string FirstName { get; set; } = "TestKub";
        public static string LastName { get; set; } = "TestKub";
        public static string AccountNumber { get; set; } = "456789456";
        public static string Email { get; set; } = "Test@Test.test";
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new GenerateQRcodePage());
            MainPage = new NavigationPage(new Views.ChildHistoryTabPage());

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
