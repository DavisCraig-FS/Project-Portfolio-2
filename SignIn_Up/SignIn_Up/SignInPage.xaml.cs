using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace SignIn_Up
{
    public partial class SignInPage : ContentPage
    {
        Dictionary<string, User> users;
        User user;
        public SignInPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            users = new Dictionary<string, User>();
            ReadFromFile();
            signInButton.Clicked += SignInButton_Clicked;
            signupButton.Clicked += SignupButton_Clicked;
            forgotUsernameButton.Clicked += ForgotUsernameButton_Clicked;
            forgotPassButton.Clicked += ForgotPassButton_Clicked;
            
        }

        async void ForgotPassButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPasswordPage());
        }

        async void ForgotUsernameButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotUsernamePage());
        }

        async void SignupButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        async void SignInButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                await DisplayAlert("Fields Incomplete!", "Make sure all fields are complete before pressing sign up.", "OKAY");
            }
            else if (!users.ContainsKey(usernameEntry.Text))
            {
                if(await DisplayAlert("Login Failed!","Username does not exist.","Sign Up","Try Again"))
                {
                    await Navigation.PushAsync(new MainPage());
                }
                ClearFields();
            }
            else if (users.ContainsKey(usernameEntry.Text) && users[usernameEntry.Text].Password != passwordEntry.Text)
            {
                await DisplayAlert("Login Failed!", "Username and Password combination is not correct.", "OKAY");
                ClearFields();
            }
            else
            {
                await Navigation.PushAsync(new HomePage());
                MessagingCenter.Send(usernameEntry.Text, "DisplayUser");
            }
        }
        private void ClearFields()
        {
            usernameEntry.Text = null;
            passwordEntry.Text = null;
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
    }
}
