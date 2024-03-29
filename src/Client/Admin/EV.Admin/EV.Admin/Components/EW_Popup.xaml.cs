﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Admin.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EW_Popup : ContentView
    {
        public EW_Popup()
        {
            InitializeComponent();
            SuccessText.BindingContext = this;
        }
        public string TextSuccess
        {
            get { return (string)GetValue(TextSuccessProperty); }
            set { SetValue(TextSuccessProperty, value); }
        }

        public static readonly BindableProperty TextSuccessProperty =
                            BindableProperty.Create(
                        propertyName: "TextSuccess",
                        returnType: typeof(string),
                        declaringType: typeof(EW_Popup),
                        defaultBindingMode: BindingMode.OneWay);
    }
}