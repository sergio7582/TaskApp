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
    public partial class AddTask : ContentPage
    {
        public AddTask(string idCategoria)
        {
            
            InitializeComponent();
            lstTasks.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }
        private void OpenMenu(object sender,EventArgs e)
        {
            App.MasterDet.IsPresented = true;
        }
        public async void TermineTask(object sender, EventArgs e)
        {

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var alumnoList = await App.SQLiteDB.GetTasksToCompleteOrNo(false, DateTime.Now.ToString("dd/MMM/yyyy"));
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
                            Message = "No tienes ninguna tarea pendiente :)"
                        },
                        BackgroundColor = Color.FromHex("#258eee"),
                        CornerRadius = 10,
                        IsRtl = true,
                        Duration = TimeSpan.FromSeconds(3)
                    };
                    await this.DisplaySnackBarAsync(options);
                    //if (answer)
                    //{
                    //    await Navigation.PushAsync(new AddTask());
                    //}
                    //else
                    //{
                    //    await Navigation.PopAsync();
                    //}


                }
            }
            catch (Exception ex)
            {
                lstTasks.IsRefreshing = false;
                Console.WriteLine(ex.Message);
                await DisplayAlert("Error", "Ocurrio un error al recuperar los datos :( ", "Ok");
                await Navigation.PopAsync();
            }

        }
    }
}