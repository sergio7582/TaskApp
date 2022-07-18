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
    public partial class AddTask : ContentPage
    {
        private int _IdCategory = 0;
        public AddTask(string Categoria)
        {
            _IdCategory = Convert.ToInt32(Categoria);
            GetTitle();
            
            InitializeComponent();
            lstTasks.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });

        }
        public async void GetTitle()
        {
            var category = await App.SQLiteDB.GetCategorysById(_IdCategory);
            Title.Text = category.Name;
        }
        private void OpenMenu(object sender,EventArgs e)
        {
            App.MasterDet.IsPresented = true;
        }
        public async void TermineTask(object sender, EventArgs e)
        {

        }
        private async void SeeForm(object sender, EventArgs e)
        {
            await btnAdd.TranslateTo(350, 0, 200);
            await bgFrame.TranslateTo(0, 0, 400);

            await txtTitle.TranslateTo(0, 0, 400);
            await txtDate.TranslateTo(0, 0, 400);
            await btnOk.TranslateTo(0, 0, 400);

        }

        private async void CreateTask(object sender, EventArgs e)
        {

            await txtTitle.TranslateTo(500, 0, 400);
            await txtDate.TranslateTo(500, 0, 400);
            await btnOk.TranslateTo(500, 0, 400);

            await bgFrame.TranslateTo(500, 0, 400);

            await btnAdd.TranslateTo(0, 0, 200);



            Tareas task = new Tareas
            {
                Tarea = txtTitle.Text,
                idCategory = _IdCategory,
                Date = txtDate.ToString(),
            };

            try
            {
                if (!string.IsNullOrEmpty(txtTitle.Text))
                {
                    txtTitle.Text = "";

                    await App.SQLiteDB.SaveTaskAsync(task);

                    OnAppearing();
                    Alert("Tarea guardada!!", "#258eee", 1);                    
                }
               
            }
            catch (Exception ex)
            {
                Alert(ex.Message, "#258eee", 2);
            }

        }
        public async void Alert(string message,string color, int time)
        {
            var options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Foreground = Color.White,
                    Message = message
                },
                BackgroundColor = Color.FromHex(color),
                CornerRadius = 3,
                IsRtl = true,
                Duration = TimeSpan.FromSeconds(time)
            };
            await this.DisplaySnackBarAsync(options);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {                

                var taskById = await App.SQLiteDB.GetTasksByIdCategory(_IdCategory);

                if(taskById.Count != 0)
                {
                    lstTasks.IsRefreshing = false;
                    lstTasks.ItemsSource = null;
                    lstTasks.ItemsSource = taskById;
                }
                

            }catch (Exception ex)
            {
                lstTasks.IsRefreshing = false;
                lstTasks.ItemsSource = null;
                Console.WriteLine(ex.Message);
            }
            //try
            //{
            //    var alumnoList = await App.SQLiteDB.GetTasksToCompleteOrNo(false, DateTime.Now.ToString("dd/MMM/yyyy"));
            //    if (alumnoList.Count != 0)
            //    {
            //        lstTasks.IsRefreshing = false;
            //        lstTasks.ItemsSource = null;
            //        lstTasks.ItemsSource = alumnoList;
            //    }
            //    else
            //    {
            //        lstTasks.IsRefreshing = false;
            //        var options = new SnackBarOptions
            //        {
            //            MessageOptions = new MessageOptions
            //            {
            //                Foreground = Color.White,
            //                Message = "No tienes ninguna tarea pendiente :)"
            //            },
            //            BackgroundColor = Color.FromHex("#258eee"),
            //            CornerRadius = 10,
            //            IsRtl = true,
            //            Duration = TimeSpan.FromSeconds(3)
            //        };
            //        await this.DisplaySnackBarAsync(options);
            //        //if (answer)
            //        //{
            //        //    await Navigation.PushAsync(new AddTask());
            //        //}
            //        //else
            //        //{
            //        //    await Navigation.PopAsync();
            //        //}


            //    }
            //}
            //catch (Exception ex)
            //{
            //    lstTasks.IsRefreshing = false;
            //    Console.WriteLine(ex.Message);
            //    await DisplayAlert("Error", "Ocurrio un error al recuperar los datos :( ", "Ok");
            //    await Navigation.PopAsync();
            //}

        }
    }
}