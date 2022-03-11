using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;

namespace SignIn_Up
{
    public partial class MainPage : ContentPage
    {
        //private string UserFile = "../../AccountInfo.txt";
        User user;
        Dictionary<string, User> users = new Dictionary<string, User>();
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            ReadFromFile();
            signUpButton.Clicked += SignUpButton_Clicked;
            signInButton.Clicked += SignInButton_Clicked;
        }

        async void SignInButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignInPage());
        }

        async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            foreach(KeyValuePair<string, User> kvp in users)
            {
                if(kvp.Value.Email == emailEntry.Text)
                {
                    await DisplayAlert("Email Taken!", "Please try a different email address.", "OKAY");
                    
                }
                ClearFields();
            }
            if(string.IsNullOrWhiteSpace(firstEntry.Text) || string.IsNullOrWhiteSpace(lastEntry.Text) || string.IsNullOrWhiteSpace(usernameEntry.Text) ||
                string.IsNullOrWhiteSpace(emailEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text) || string.IsNullOrWhiteSpace(retypePassEntry.Text))
            {
                await DisplayAlert("Fields Incomplete!", "Make sure all fields are complete before pressing sign up.", "OKAY");
            }
            else if (users.ContainsKey(usernameEntry.Text))
            {
                await DisplayAlert("Username Taken!", "Please try a different username.", "OKAY");
                ClearFields();
            }
            else if (!emailEntry.Text.Contains('@'))
            {
                await DisplayAlert("Invalid Email!", "Please enter a valid email address.", "OKAY");
                ClearFields();
            }
            else if (passwordEntry.Text != retypePassEntry.Text)
            {
                await DisplayAlert("Passwords Don't Match!", "Please make sure the passwords are the same.", "OKAY");
                ClearFields();
            }
            else
            {
                await DisplayAlert("Success!","You have created an account.","OKAY");
                
                if (!File.Exists(App.FilePath))
                {
                    using (StreamWriter sw = File.CreateText(App.FilePath))
                    {
                        sw.WriteLine($"{firstEntry.Text}|{lastEntry.Text}|{usernameEntry.Text}|{emailEntry.Text}|{passwordEntry.Text}");
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(App.FilePath))
                    {
                        sw.WriteLine($"{firstEntry.Text}|{lastEntry.Text}|{usernameEntry.Text}|{emailEntry.Text}|{passwordEntry.Text}");
                    }
                }
                
                await Navigation.PushAsync( new SignInPage());
                
            }
            
        }
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
                        string[] data = line.Split('|');
                        user = new User
                        {
                            FirstName = data[0],
                            LastName = data[1],
                            UserName = data[2],
                            Email = data[3],
                            Password = data[4]
                        };
                        users.Add(data[2], user);
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
