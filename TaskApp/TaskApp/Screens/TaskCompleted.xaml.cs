using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskApp.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskCompleted : ContentPage
    {
        public TaskCompleted()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var alumnoList = await App.SQLiteDB.GetTasksToCompleteOrNo(true, DateTime.Now.ToString("dd/MMM/yyyy"));
                if (alumnoList.Count != 0)
                {
                    lstTasks.IsRefreshing = false;
                    lstTasks.ItemsSource = null;
                    lstTasks.ItemsSource = alumnoList;
                }
                else
                {
                    lstTasks.IsRefreshing = false;
                    var options = new SnackBarOptions
                    {
                        MessageOptions = new MessageOptions
                        {
                            Foreground = Color.White,
                            Message = "No tienes ninguna tarea terminada :("
                        },
                        BackgroundColor = Color.FromHex("#258eee"),
                        CornerRadius = 3,
                        IsRtl = true,
                        Duration = TimeSpan.FromSeconds(3)
                    };
                    await this.DisplaySnackBarAsync(options);

                }
            }
            catch (Exception ex)
            {
                lstTasks.IsRefreshing = false;
                Console.WriteLine(ex.Message);
                var options = new SnackBarOptions
                {
                    MessageOptions = new MessageOptions
                    {
                        Foreground = Color.White,
                        Message = "Ocurrio un error al recuperar los datos :("
                    },
                    BackgroundColor = Color.FromHex("#ff4b5f"),
                    CornerRadius = 3,
                    IsRtl = true,
                    Duration = TimeSpan.FromSeconds(3)
                };
                await this.DisplaySnackBarAsync(options);
                await Navigation.PopAsync();
            }

        }
    }
}