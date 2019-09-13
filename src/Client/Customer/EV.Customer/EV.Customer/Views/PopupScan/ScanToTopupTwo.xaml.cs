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
    public partial class ScanToTopupTwo : Rg.Plugins.Popup.Pages.PopupPage
    {
        public ScanToTopupTwo(decimal amount , string adminName, string accountNumber, string qrcodeReference)
        {
            InitializeComponent();
            TopUpViewModel topUp = new TopUpViewModel()
            {
                Amount = amount,
                AdminName = adminName,
                AdminAccountNumber = accountNumber,
                QRCodeReference = qrcodeReference
            };
            BindingContext = topUp;
        }
    }
}