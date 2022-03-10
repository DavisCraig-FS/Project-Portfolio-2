using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SignIn_Up
{
    public partial class App : Application
    {
        public static string FilePath { get; private set; }
        public static string ActivePath { get; private set; }
        public App()
        {
            InitializeComponent();
            FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)) + "UserAccounts.txt";
            MainPage = new NavigationPage(new MainPage());

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