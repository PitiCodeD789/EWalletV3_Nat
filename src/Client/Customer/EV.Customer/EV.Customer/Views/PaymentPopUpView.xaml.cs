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
    public partial class PaymentPopUpView : Rg.Plugins.Popup.Pages.PopupPage
    {
        public PaymentPopUpView(string merchantName, string merchantAccount)
        {
            InitializeComponent();
            (BindingContext as PaymentPageViewModel).MerchantName = merchantName;
            (BindingContext as PaymentPageViewModel).MerchantAccountNumber = merchantAccount;

        }
    }
}