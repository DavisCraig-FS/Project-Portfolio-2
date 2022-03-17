using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace Alpha
{
    public partial class App : Application
    {
        // property FilePath for the app class
        public static string UserFilePath { get; private set; }
        public static string FavoriteFilePath { get; private set; }
        public static bool SignedIn { get; set; }
        public App()
        {
            InitializeComponent();
            // assign a path for the local text files 
            UserFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)) + "Accounts.txt";
            FavoriteFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)) + "FavoritesList.txt";
            // create a boolean variable to store the value if the user 
            // is signed in or not using built in storage properties
            bool signedIn = Current.Properties.ContainsKey("SignedIn") ? Convert.ToBoolean(Current.Properties["SignedIn"]) : false;
            // if the user is still signed in
            if (!signedIn)
            {
                // load the homepage
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                // if they are not, load the mainpage
                MainPage = new NavigationPage(new HomePage());
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
