using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;

namespace Alpha
{
    public partial class MainPage : ContentPage
    {
        // Variables to hold user data
        User user;
        List<string> emails = new List<string>();
        Dictionary<string, User> users = new Dictionary<string, User>();
        public MainPage()
        {
            InitializeComponent();
            // Remove the back button on the nav bar
            NavigationPage.SetHasNavigationBar(this, false);
            // call the read from file method
            ReadFromFile();
            // Events
            signUpButton.Clicked += SignUpButton_Clicked;
            signInButton.Clicked += SignInButton_Clicked;
        }
        // Event method for sign in button
        async void SignInButton_Clicked(object sender, EventArgs e)
        {
            // navigate to sign in page when button is pressed
            await Navigation.PushAsync(new SignInPage());
        }
        // Event method for sign up button
        async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            // if any of the fields are empty
            if (string.IsNullOrWhiteSpace(firstEntry.Text) || string.IsNullOrWhiteSpace(lastEntry.Text) || string.IsNullOrWhiteSpace(usernameEntry.Text) ||
                    string.IsNullOrWhiteSpace(emailEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text) || string.IsNullOrWhiteSpace(retypePassEntry.Text))
            {
                // display the alert
                await DisplayAlert("Fields Incomplete!", "Make sure all fields are complete before pressing sign up.", "OKAY");
                // call the method for clearing all the entry fields
                ClearFields();
            }
            // if the email is already exists
            else if (emails.Contains(emailEntry.Text))
            {
                // display the alert
                await DisplayAlert("Email Taken!", "Please try a different email address.", "OKAY");
                // call the method for clearing all the entry fields
                ClearFields();
            }
            // if the username already exists
            else if (users.ContainsKey(usernameEntry.Text))
            {
                // display the alert
                await DisplayAlert("Username Taken!", "Please try a different username.", "OKAY");
                ClearFields();
            }
            // if the email is not the right format
            else if (!emailEntry.Text.Contains('@'))
            {
                // display the alert
                await DisplayAlert("Invalid Email!", "Please enter a valid email address.", "OKAY");
                ClearFields();
            }
            // if the passwords do not match
            else if (passwordEntry.Text != retypePassEntry.Text)
            {
                // display the alert
                await DisplayAlert("Passwords Don't Match!", "Please make sure the passwords are the same.", "OKAY");
                ClearFields();
            }
            // if it all works 
            else
            {
                // display the alert
                await DisplayAlert("Success!", "You have created an account.", "OKAY");
                // if the file does not exist
                if (!File.Exists(App.UserFilePath))
                {
                    // create the file 
                    using (StreamWriter sw = File.CreateText(App.UserFilePath))
                    {
                        // write the user info to the file
                        sw.WriteLine($"{firstEntry.Text}|{lastEntry.Text}|{usernameEntry.Text}|{emailEntry.Text}|{passwordEntry.Text}");
                    }
                }
                // if the file does exist
                else
                {
                    // append the file
                    using (StreamWriter sw = File.AppendText(App.UserFilePath))
                    {
                        // write the user info to the file
                        sw.WriteLine($"{firstEntry.Text}|{lastEntry.Text}|{usernameEntry.Text}|{emailEntry.Text}|{passwordEntry.Text}");
                    }
                }
                // navigate to a new sign in page
                await Navigation.PushAsync(new SignInPage());
            }
        }

        public void ReadFromFile()
        {
            if (File.Exists(App.UserFilePath))
            {
                // Opening a streamreader to read all file contents
                using (StreamReader sr = new StreamReader(App.UserFilePath))
                {
                    // variable to hold read line 
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
                        // add the new user to the dictionary and the email to list
                        users.Add(data[2], user);
                        emails.Add(data[3]);
                    }
                }

            }
        }
        // Method for clearing the field values
        private void ClearFields()
        {
            firstEntry.Text = null;
            lastEntry.Text = null;
            usernameEntry.Text = null;
            emailEntry.Text = null;
            passwordEntry.Text = null;
            retypePassEntry.Text = null;
        }
    }
}
