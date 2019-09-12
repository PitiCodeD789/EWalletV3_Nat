using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Customer.Components
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
        public ICommand PushCommand
        {
            get { return (ICommand)GetValue(PushCommandProperty); }
            set { SetValue(PushCommandProperty, value); }
        }

        public static readonly BindableProperty PushCommandProperty =
                            BindableProperty.Create(
                        propertyName: "PushCommand",
                        returnType: typeof(ICommand),
                        declaringType: typeof(EW_Popup),
                        defaultBindingMode: BindingMode.OneWay);       

        private void PopupButton_Clicked(object sender, EventArgs e)
        {
            PushCommand?.Execute(null);
        }
    }
}