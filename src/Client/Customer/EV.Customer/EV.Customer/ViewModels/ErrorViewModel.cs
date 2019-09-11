using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EV.Customer.ViewModels
{
    public class ErrorViewModel
    {
        public Action MyAction { get; set; }

        public ErrorViewModel()
        {
            TextError = "ทำรายการไม่สำเร็จ";
            MyAction = Pop;

            ClosePopup = new Command(PopPopup);
        }

        public ErrorViewModel(string title)
        {
            TextError = title;
            MyAction = Pop;

            ClosePopup = new Command(PopPopup);
        }

        public ErrorViewModel(string title, Action action)
        {
            TextError = title;
            MyAction = action == null ? Pop : action;

            ClosePopup = new Command(PopPopup);
        }
        public ICommand ClosePopup { get; set; }
        public void PopPopup()
        {
            MyAction?.Invoke();
        }

        public void Pop()
        {
            Application.Current.MainPage.Navigation.PopPopupAsync();
        }
        public string TextError { get; set; }
    }
}
