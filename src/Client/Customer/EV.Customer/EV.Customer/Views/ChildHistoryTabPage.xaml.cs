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
    public partial class ChildHistoryTabPage : ContentPage
    {
        private HomePageViewModel vm;

        public ChildHistoryTabPage()
        {
            InitializeComponent();
            vm = new HomePageViewModel();
            BindingContext = vm;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await vm.GetTotalBalance();
        }
    }
}