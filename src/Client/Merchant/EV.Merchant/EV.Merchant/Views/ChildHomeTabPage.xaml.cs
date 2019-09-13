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
    public partial class ChildHomeTabPage : ContentPage
    {
        public ChildHomeTabPage()
        {
            InitializeComponent();
        }

        private void TapCreateQRCode_Tapped(object sender, EventArgs e)
        {

        }
    }
}