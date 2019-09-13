using EV.Merchant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Merchant.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChildHistoryTabPage : ContentPage
    {
        private MerchantTransactionModel vm;
        public ChildHistoryTabPage()
        {
            InitializeComponent();

            vm = new MerchantTransactionModel();
            BindingContext = vm;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

           await vm.GetTransactions();
        }
    }
}