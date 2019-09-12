using EV.Admin.Views;
using EWalletV2.Api.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using static EWalletV2.Api.ViewModels.EW_Enumerations;

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
            ErrorViewModel errorViewModel = new ErrorViewModel("ทำรายการเกือบสำเร็จ", (int)EW_Enumerations.EW_ErrorTypeEnum.Warning);

            PopupNavigation.Instance.PushAsync(new Error(errorViewModel));
        }

        public void MMMM()
        {
            App.Current.MainPage.DisplayAlert("","dfghjk","ok");
        }
    }
}
