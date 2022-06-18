using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskApp.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        private async void AddTask(object sender,EventArgs e)
        {
            await Navigation.PushAsync(new AddTask());
        }

        private async void GoToPendingTasks(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PendingTasks());
        }

        private async void GoToTaskCompleted(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskCompleted());
        }
        private async void GoToUrgentTasks(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UrgentTasks());
        }
        private async void GoToHistory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new History());
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var TasksComplete = await App.SQLiteDB.GetTasksToCompleteOrNo(true,DateTime.Now.ToString("dd/MMM/yyyy"));

                var TasksPending = await App.SQLiteDB.GetTasksToCompleteOrNo(false,DateTime.Now.ToString("dd/MMM/yyyy"));

                var TaskUrgents = await App.SQLiteDB.GetTasksUrgents(DateTime.Now.ToString("dd/MMM/yyyy"));

                pendingTasks.Text = TasksPending.Count.ToString();
                completeTasks.Text = TasksComplete.Count.ToString();
                urgentTasks.Text = TaskUrgents.Count.ToString();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

    }
}