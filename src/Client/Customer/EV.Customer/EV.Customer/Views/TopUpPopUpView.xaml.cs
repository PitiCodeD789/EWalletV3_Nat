using EV.Customer.ViewModels;
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
    public partial class TopUpPopUpView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public TopUpPopUpView(decimal amount, string adminName, string accountNumber, string qrcodeReference)
        {
            InitializeComponent();
            (BindingContext as TopUpViewModel).Amount = amount;
            (BindingContext as TopUpViewModel).AdminName = adminName;
            (BindingContext as TopUpViewModel).AdminAccountNumber = accountNumber;
            (BindingContext as TopUpViewModel).QRCodeReference = qrcodeReference;

        }
    }
}