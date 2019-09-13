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
    public partial class TransactionsOne : Rg.Plugins.Popup.Pages.PopupPage
    {
        public TransactionsOne(CustomerTransactionViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            //return base.OnBackButtonPressed();

            return true;
        }
    }
}