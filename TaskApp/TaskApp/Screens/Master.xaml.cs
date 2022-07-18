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
    public partial class Master : ContentPage
    {
        
        public Master()
        {
            InitializeComponent();
            lstCategorys.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }
        private async void GoToAddTask(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new AddTask("Trabajo"), false);
        }
        private void SeeForm(object sender,EventArgs e)
        {
            btnAdd.IsVisible = false;
            NewCategory.IsVisible = true;
        }
        private async void GoToHome(object sender, EventArgs e)
        {
            var mi = ((Button)sender);
            if(mi.CommandParameter.ToString() != "0")
            {
                App.MasterDet.IsPresented = false;
                await App.MasterDet.Detail.Navigation.PushAsync(new AddTask(mi.CommandParameter.ToString()), false);
            }
            else
            {
                App.MasterDet.IsPresented = false;
                await App.MasterDet.Detail.Navigation.PushAsync(new Home(), false);
            }           
        }
        private async void DeleteCategory(object sender, EventArgs e)
        {
            var mi = ((ToolbarItem)sender);

            await App.SQLiteDB.DeleteCategory(Convert.ToInt32(mi.CommandParameter));
            
            OnAppearing();

            Alert("Categoria borrada", 1, "#258eee");
            
        }

        public async void Alert(string message, int time,string color)
        {
            var options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Foreground = Color.White,
                    Message = message
                },
                BackgroundColor = Color.FromHex(color),
                CornerRadius = 10,
                IsRtl = true,
                Duration = TimeSpan.FromSeconds(time)
            };
            await this.DisplaySnackBarAsync(options);
        }

        private async void CreateCategory(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(titleCategory.Text)) {

                Categorys categorys = new Categorys
                {
                    Name = titleCategory.Text,
                };
                try
                {
                    await App.SQLiteDB.SaveCategoryAsync(categorys);

                    OnAppearing();
                    btnAdd.IsVisible = true;
                    NewCategory.IsVisible = false;
                    var options = new SnackBarOptions
                    {
                        MessageOptions = new MessageOptions
                        {
                            Foreground = Color.White,
                            Message = "Categoria creada!!"
                        },
                        BackgroundColor = Color.FromHex("#258eee"),
                        CornerRadius = 10,
                        IsRtl = true,
                        Duration = TimeSpan.FromSeconds(1)
                    };
                    await this.DisplaySnackBarAsync(options);
                    titleCategory.Text = "";
                    

                }
                catch(Exception ex) {
                    Console.WriteLine(ex.Message);

                }
                


            }
            else
            {
                btnAdd.IsVisible = true;
                NewCategory.IsVisible = false;
            }
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var LstCategory = await App.SQLiteDB.GetCategorysAsync();

                List<Categorys> categorys = new List<Categorys>
                {
                    new Categorys { IdCategory = 0, Name = "Tu día" }                      
                };
                
                categorys.AddRange(LstCategory);
                

                lstCategorys.ItemsSource = null;
                lstCategorys.IsRefreshing = false;
                lstCategorys.ItemsSource = categorys;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



        }
    }
}