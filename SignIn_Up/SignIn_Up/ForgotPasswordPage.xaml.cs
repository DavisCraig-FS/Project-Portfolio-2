using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;

// Craig Davis
// C202203
// MDV228
// 2.1 CODE: Sign-In/ Sign-Up

namespace SignIn_Up
{
    public partial class ForgotPasswordPage : ContentPage
    {
        // Variables to hold user data
        User user;
        Dictionary<string, User> users = new Dictionary<string, User>();
        public ForgotPasswordPage()
        {
            InitializeComponent();
            // remove back button from the nav bar
            NavigationPage.SetHasNavigationBar(this, false);
            // call the read from file method
            ReadFromFile();
            // events
            forgotPassButton.Clicked += ForgotPassButton_Clicked;
            closeButton.Clicked += CloseButton_Clicked;
        }
        // event method for close button
        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            // navigate back to sign in page
            Navigation.PopAsync();
        }
        // event method for forgot password button
        async void ForgotPassButton_Clicked(object sender, EventArgs e)
        {
            // if the field is empty
            if (string.IsNullOrWhiteSpace(forgotUsernameEntry.Text))
            {
                // display the alert
                await DisplayAlert("Field Incomplete!", "Please type a valid username.", "OKAY");
            }
            // if the username does not exist
            else if (!users.ContainsKey(forgotUsernameEntry.Text))
            {
                // display the alert/ if the user selects the sign up option
                if (await DisplayAlert("Invalid Entry!", "Username does not exist.", "Sign Up", "Try Again"))
                {
                    // navigate to the sign up/ main page
                    await Navigation.PushAsync(new MainPage());
                }
            }
            // assign the password label text value to the password for the the username entered
            else
            { 
                passwordLabel.Text = users[forgotUsernameEntry.Text].Password;
            }
        }
        // Method to read from the text file and load data
        public void ReadFromFile()
        {
            if (File.Exists(App.FilePath))
            {
                // Opening a streamreader to read all file contents
                using (StreamReader sr = new StreamReader(App.FilePath))
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
