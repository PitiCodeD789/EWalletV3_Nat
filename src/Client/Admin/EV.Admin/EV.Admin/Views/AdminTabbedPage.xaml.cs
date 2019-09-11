using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace EV.Admin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminTabbedPage : Xamarin.Forms.TabbedPage
    {
        public AdminTabbedPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarSelectedItemColor(Color.Orange);
            Children.Add(new ChildHomeTabPage());
            Children.Add(new ChildHistoryTabPage());
            Children[0].Title = "หน้าแรก";
            Children[0].IconImageSource = "home";
            Children[1].Title = "ประวัติการใช้งาน";
            Children[1].IconImageSource = "history";
        }
    }
}