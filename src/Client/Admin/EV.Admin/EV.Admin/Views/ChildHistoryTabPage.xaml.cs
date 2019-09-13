using EV.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Admin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChildHistoryTabPage : ContentPage
    {
        private AdminTransactionViewModel vm;
        public ChildHistoryTabPage()
        {
            InitializeComponent();

            vm = new AdminTransactionViewModel();
            BindingContext = vm;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await vm.GetTransactions();
        }
    }
}