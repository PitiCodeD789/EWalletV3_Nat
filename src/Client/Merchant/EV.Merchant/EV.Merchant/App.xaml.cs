﻿using EV.Merchant.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Merchant
{
    public partial class App : Application
    {
        public static string Account { get; set; }
        public static string Username { get; set; }
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string PhoneNumber { get; set; }
        public static string Email { get; set; } 
        public static string StoreName { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
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
