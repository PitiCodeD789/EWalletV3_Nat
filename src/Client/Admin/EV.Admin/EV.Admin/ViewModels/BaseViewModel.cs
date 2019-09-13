using EV.Admin.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EV.Admin.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel()
        {
            BackButton = new Command(BackPageMethod);
        }

        public virtual ICommand BackButton { get; set; }
        public async virtual void BackPageMethod()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public virtual void ForceLogout()
        {
            SecureStorage.RemoveAll();
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        public virtual void ForceLogoutForErrorPopup()
        {
            SecureStorage.RemoveAll();
            Application.Current.MainPage.Navigation.PopPopupAsync();
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        public virtual void CloseApp()
        {
            SecureStorage.RemoveAll();
            Environment.Exit(0);
        }

        public virtual event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
