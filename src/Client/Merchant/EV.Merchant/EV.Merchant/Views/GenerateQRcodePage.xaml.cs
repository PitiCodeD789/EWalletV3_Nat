using EV.Merchant.ViewModels;
using EWalletV2.Api.ViewModels.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Merchant.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenerateQRcodePage : ContentPage
    {
        public GenerateQRcodePage(string merchantAccount,string fullname)
        {
            InitializeComponent();
            AccountCommand account = new AccountCommand();
            account.AccountNumber = merchantAccount;
            var topupJson = JsonConvert.SerializeObject(account);
            (BindingContext as GenerateQRcodeViewModel).QrcodeData = topupJson;
            (BindingContext as GenerateQRcodeViewModel).FullName = fullname;

        }
    }
}