using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Craig Davis
// C202203
// MDV228
// 2.1 CODE: Sign-In/ Sign-Up

namespace SignIn_Up
{
    public partial class App : Application
    {
        // property for the app class
        public static string FilePath { get; private set; }
        public App()
        {
            InitializeComponent();
            // path for the local text file 
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