using EV.Admin.ViewModels;
using EWalletV2.Api.ViewModels.Transaction;
using Newtonsoft.Json;
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
    public partial class QRcodePage : ContentPage
    {
        public QRcodePage(GenerateTopupViewModel topupData)
        {
            InitializeComponent();
            (BindingContext as GenerateQRcodeViewModel).DisplayData = topupData;
            var topupJson = JsonConvert.SerializeObject(topupData);
            (BindingContext as GenerateQRcodeViewModel).QrcodeData = topupJson;
        }
    }
}