using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class SettingPageViewModel
    {
        public SettingPageViewModel()
        {
            ChangePinCommand = new Command(ChangePin);
        }

        public ICommand ChangePinCommand { get; set; }
        private void ChangePin()
        {
            //ไปหน้าเปลี่ยนพาสเวริด
        }

        public ICommand EnableFingerPrintCommand { get; set; }
        private void EnableFingerPrint()
        {
            
        }

    }
}
