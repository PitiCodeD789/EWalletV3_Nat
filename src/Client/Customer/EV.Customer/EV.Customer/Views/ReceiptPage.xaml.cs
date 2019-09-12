﻿using EWalletV2.Api.ViewModels.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Customer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceiptPage : ContentPage
    {
        public ReceiptPage(SlipViewModel paymentSlip)
        {
            InitializeComponent();
            BindingContext = paymentSlip;
        }
    }
}