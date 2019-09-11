using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Merchant.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EW_TitleEntryView : ContentView
    {
        public EW_TitleEntryView()
        {
            InitializeComponent();
            mEntry.BindingContext = this;
        }

        #region TextEntry
        public string TextEntry
        {
            get { return (string)GetValue(TextEntryProperty); }
            set { SetValue(TextEntryProperty, value); }
        }

        public static readonly BindableProperty TextEntryProperty =
            BindableProperty.Create(
                propertyName: "TextEntry",
                returnType: typeof(string),
                declaringType: typeof(EW_TitleEntryView),
                defaultBindingMode: BindingMode.TwoWay);
        #endregion


        #region TextTitle
        private string textTitle;

        public string TextTitle
        {
            get { return textTitle; }
            set
            {
                textTitle = value;
                mLabel.Text = textTitle;
            }
        }
        #endregion

        #region KeyboardType
        private Keyboard keyboardType;

        public Keyboard KeyboardType
        {
            get { return keyboardType; }
            set
            {
                keyboardType = value;
                mEntry.Keyboard = keyboardType;
            }
        }

        #endregion

        #region IsEnable
        public bool IsEntryEnabled
        {
            get { return (bool)GetValue(isEntryEnabledProperty); }
            set { SetValue(isEntryEnabledProperty, value); }
        }

        public static readonly BindableProperty isEntryEnabledProperty =
            BindableProperty.Create(
                propertyName: "IsEntryEnabled",
                returnType: typeof(bool),
                declaringType: typeof(EW_TitleEntryView),
                defaultBindingMode: BindingMode.TwoWay);
        #endregion
    }
}