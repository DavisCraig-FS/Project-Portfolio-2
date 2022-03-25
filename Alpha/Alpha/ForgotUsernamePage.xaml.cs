using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;

namespace Alpha
{
    public partial class ForgotUsernamePage : ContentPage
    {
        // Variables to hold user data
        Dictionary<string, User> users;
        private List<User> usernames = new List<User>();
        User user;
        public ForgotUsernamePage()
        {
            InitializeComponent();
            // remove the back button from the nav bar
            NavigationPage.SetHasNavigationBar(this, false);
            // instantiate a new dictionary
            users = new Dictionary<string, User>();
            // call read from file method
            ReadFromFile();
            // assign listview items source to usernames list
            listView.ItemsSource = usernames;
            // loop through the dictionary and add usernames to the list
            foreach (KeyValuePair<string, User> kvp in users)
            {
                usernames.Add(kvp.Value);
            }
            // create a datatemplate for the text cell and set the binding
            DataTemplate dt = new DataTemplate(typeof(TextCell));
            dt.SetBinding(TextCell.TextProperty, new Binding("UserName"));
            dt.SetValue(TextCell.TextColorProperty, Color.White);
            listView.ItemTemplate = dt;
            // event 
            closeButton.Clicked += CloseButton_Clicked;
        }
        // event method for close button
        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            // navigate back to sign in page
            Navigation.PopAsync();
        }
        // method for on appearing
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // assign image to background
            BackgroundImageSource = "Gradient.png";
            // assign listview source to usernames list and display
            listView.ItemsSource = usernames.ToList();
        }
        public void ReadFromFile()
        {
            if (File.Exists(App.UserFilePath))
            {
                // Opening a streamreader to read all file contents
                using (StreamReader sr = new StreamReader(App.UserFilePath))
                {
                    // variable to hold read line (stock symbol)
                    string line;
                    // looping for every readline that exists
                    while ((line = sr.ReadLine()) != null)
                    {
                        // create an array and add data
                        string[] data = line.Split('|');
                        // instantiate a new user object and add all info
                        user = new User
                        {
                            FirstName = data[0],
                            LastName = data[1],
                            UserName = data[2],
                            Email = data[3],
                            Password = data[4]
                        };
                        // add the new user to the dictionary
                        users.Add(data[2], user);
                    }
                }
            }
        }
    }
}
