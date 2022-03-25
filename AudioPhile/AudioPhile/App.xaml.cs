using System;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("MontserratAlternates-Regular.ttf", Alias = "AppFont")]
namespace AudioPhile
{
    public partial class App : Application
    {
        // property FilePath for the app class
        public static string UserFilePath { get; private set; }
        public static bool SignedIn { get; set; }

        public App()
        {
            InitializeComponent();
            // assign a path for the local file
            UserFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)) + "UserFiles.txt";
            // create a boolean variable to store the value if the user 
            // is signed in or not using built in local storage properties
            bool signedIn = Current.Properties.ContainsKey("SignedIn") ? Convert.ToBoolean(Current.Properties["SignedIn"]) : false;
            // if the user is not signed in
            if (!signedIn)
            {
                // load the mainpage
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                // if they are, load the homepage
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
