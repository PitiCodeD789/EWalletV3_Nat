using EV.Admin.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Admin.ViewModels
{
   public  class Class1
    {
        public Class1()
        {
            PushPopup = new Command(Push);
        }
        public ICommand PushPopup { get; set; }
        public void Push()
        {
            ErrorViewModel errorViewModel = new ErrorViewModel("รายว้า",MMMM);

            PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
        }

        public void MMMM()
        {
            App.Current.MainPage.DisplayAlert("","dfghjk","ok");
        }
    }
}
