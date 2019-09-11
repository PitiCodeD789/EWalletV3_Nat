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
    public partial class ChildHomeTabPage : ContentPage
    {
        public ChildHomeTabPage()
        {
            InitializeComponent();
        }

        private void TapPayment_Tapped(object sender, EventArgs e)
        {

        }

        private void TapTopupFromAdminApp_Tapped(object sender, EventArgs e)
        {

        }
    }
}