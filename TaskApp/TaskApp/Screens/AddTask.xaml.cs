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
        public AddTask()
        {
            InitializeComponent();
        }
        public async void SaveTask(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                Tareas task = new Tareas
                {
                    Title = Title.Text,
                    Description = Description.Text,
                    Date = DateLimit.Date.ToString("dd/MMM/yyyy"),
                    Hour = Hour.Time.ToString(),
                    IsComplete = false,

                };
                try
                {
                    await App.SQLiteDB.SaveTaskAsync(task);
                    Title.Text = "";
                    Description.Text = "";
                    var options = new SnackBarOptions
                    {
                        MessageOptions = new MessageOptions
                        {
                            Foreground = Color.White,
                            Message = "Tarea guardada!!"
                        },
                        BackgroundColor = Color.FromHex("#258eee"),
                        CornerRadius = 3,
                        IsRtl = true,
                        Duration = TimeSpan.FromSeconds(3)
                    };
                    await this.DisplaySnackBarAsync(options);                                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    var options = new SnackBarOptions
                    {
                        MessageOptions = new MessageOptions
                        {
                            Foreground = Color.White,
                            Message = "Ocurrio un error, vuelve a intentarlo"
                        },
                        BackgroundColor = Color.FromHex("#ff4b5f"),
                        CornerRadius = 3,
                        IsRtl = true,
                        Duration = TimeSpan.FromSeconds(3.5)
                    };
                    await this.DisplaySnackBarAsync(options);
                    
                }                
            }
            else
            {
                var options = new SnackBarOptions
                {
                    MessageOptions = new MessageOptions
                    {
                        Foreground = Color.White,
                        Message = "Verifica los campos"
                    },
                    BackgroundColor = Color.FromHex("#ff9900"),
                    CornerRadius = 3,
                    IsRtl = true,
                    Duration = TimeSpan.FromSeconds(3)
                };
                await this.DisplaySnackBarAsync(options);
            }
        }
        public bool ValidateData()
        {
            if(string.IsNullOrEmpty(Title.Text) || string.IsNullOrEmpty(Description.Text))
            {
                return false;
            }
            else if (DateLimit.Date == null)
            {
                return false;
            }else if (DateLimit.Date < DateTime.Now.AddDays(-1))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}