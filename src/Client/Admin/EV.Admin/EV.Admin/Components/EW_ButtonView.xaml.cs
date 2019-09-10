using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EV.Admin.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EW_ButtonView : ContentView
    {
        public EW_ButtonView()
        {
            InitializeComponent();
        }

        #region CommandButton
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                propertyName: "Command",
                returnType: typeof(ICommand),
                declaringType: typeof(EW_ButtonView)); //คนที่ประกาศ

        private void MButton_Clicked(object sender, EventArgs e)
        {
            //if (Command!=null)
            //{
            //    Command.Execute(null);
            //} รูปเต็ม

            Command?.Execute(null);

            //Execute คือเรียกใช้ method ซึ่งคือ ICommand  null คือ value ของ command

        }

        #endregion

        #region TextButton

        private string textButton;

        public string TextButton
        {
            get { return textButton; }
            set
            {
                textButton = value;
                mButton.Text = textButton;
            }
        }


        //หรือแบบนี้ถ้าจะ Binding
        //public string TextButton { get; set; }

        //public static readonly BindableProperty TextButtonProperty =
        //    BindableProperty.Create(
        //        propertyName:"TextButton", 
        //        returnType: typeof(string), 
        //        declaringType: typeof(EW_ButtonView), //คนที่ประกาศ
        //        defaultValue: "", 
        //        propertyChanged: OnTextButtonChanged);

        //private static void OnTextButtonChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    if (newValue != null && !string.IsNullOrEmpty((string)newValue))
        //    {
        //        var control = bindable as EW_ButtonView;
        //        control.mButton.Text = (string)newValue;
        //    }
        //}
        #endregion
    }
}