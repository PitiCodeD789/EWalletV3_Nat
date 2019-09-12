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
    public partial class TopupPage : ContentPage
    {
        private int textLength = 0;
        private int currentMargin = -20;
 
        public TopupPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            inputMoney.Focus();
        }

        private void TextSizeChange(object sender, TextChangedEventArgs e)
        {
        
            if(textLength <= 7) {
            if(textLength< inputMoney.Text.Length)
            {
                currentMargin += 2;
                inputMoney.WidthRequest += 10;
            }
            else
            {
                currentMargin -= 2;
                inputMoney.WidthRequest -= 10;
            }
            textLength = inputMoney.Text.Length;
            thbTag.Margin = new Thickness(currentMargin, 20, 0, 0);
            }

        }
    }
}