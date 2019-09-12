﻿using EV.Merchant.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Merchant
{
    public partial class App : Application
    {
        public static string Email = "";
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new GenerateQRcodePage());
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
