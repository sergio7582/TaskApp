using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TaskApp.Screens;
using TaskApp.Data;
using System.IO;

namespace TaskApp
{
    public partial class App : Application
    {
        static SQLiteHelper db;
        public static MasterDetailPage MasterDet { get; set; }
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }
        public static SQLiteHelper SQLiteDB
        {
            get
            {
                if (db == null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Tasks.db3"));
                }
                return db;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
