using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace Alpha
{
    public partial class App : Application
    {
        // property FilePath for the app class
        public static string FilePath { get; private set; }
        public App()
        {
            InitializeComponent();
            // assign a path for the local text file 
            FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)) + "Accounts.txt";
            // wrap the mainpage in a navigation page to give navigation attributes
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
