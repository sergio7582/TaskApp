using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Screens;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.Master = new Master();
            this.Detail = new NavigationPage(new Home());
            
            Detail.BackgroundColor = Color.FromHex("#C6DCE4");
            
            

            App.MasterDet = this;
        }
    }
}