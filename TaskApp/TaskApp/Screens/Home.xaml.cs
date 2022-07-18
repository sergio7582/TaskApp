using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Models;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskApp.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        
        public Home()
        {            
            InitializeComponent();
            lstTasks.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }
        public void OpenMenu(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = true;
        }
        public async void TermineTask(object sender, EventArgs e)
        {

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            lstTasks.IsRefreshing = false;
            var lstComplete = new List<LstHome>();

            var LstTaskToday = await App.SQLiteDB.GetTaskByDay(DateTime.Now.ToString("dd/MMM/yyyy"));
            var lstToday = new List<LstHome>()
            {
                new LstHome()
                {
                    Heading = "Hoy",
                    Tareas = LstTaskToday.ToList()
                }
            };

            var lstTaskTomorrow = await App.SQLiteDB.GetTaskByDay(DateTime.Now.AddDays(1).ToString("dd/MMM/yyyy"));

            var lstTomorrow = new List<LstHome>()
            {
                new LstHome()
                {
                    Heading = "Mañana",
                    Tareas = lstTaskTomorrow.ToList()
                }
            };

            lstComplete.AddRange(lstToday);
            lstComplete.AddRange(lstTomorrow);

            





        }
    }
}