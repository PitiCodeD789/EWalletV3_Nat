using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace EV.Customer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserTabbedPage : Xamarin.Forms.TabbedPage
    {
        public UserTabbedPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarSelectedItemColor(Color.Orange);
            Children.Add(new ChildHomeTabPage());
            Children.Add(new TransactionsPage());
            Children.Add(new ChildProfileTabPage());
            Children[0].Title = "หน้าแรก";
            Children[0].IconImageSource = "home";
            Children[1].Title = "ประวัติการใช้งาน";
            Children[1].IconImageSource = "history";
            Children[2].Title = "ข้อมูลส่วนตัว";
            Children[2].IconImageSource = "user";
        }
    }
}