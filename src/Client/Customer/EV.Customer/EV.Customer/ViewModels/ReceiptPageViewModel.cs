using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class ReceiptPageViewModel : BaseViewModel
    {
        public ReceiptPageViewModel()
        {
            PopToRootCommand = new Command(() => Application.Current.MainPage.Navigation.PopToRootAsync());
        }

        public Command PopToRootCommand { get; set; }
    }
}
