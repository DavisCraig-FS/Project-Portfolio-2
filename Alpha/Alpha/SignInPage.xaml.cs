using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using System.Diagnostics;

namespace Alpha
{
    public partial class SignInPage : ContentPage
    {
        // Variables to hold user data
        Dictionary<string, User> users;
        User user;
        public SignInPage()
        {
            InitializeComponent();
            // Remove the back button on the nav bar
            NavigationPage.SetHasNavigationBar(this, false);
            // instatiate a new dictionart object
            users = new Dictionary<string, User>();
            // call the read from file method
            ReadFromFile();
            logoImage.Source = "APLogo.png";
            // events
            signInButton.Clicked += SignInButton_Clicked;
            signupButton.Clicked += SignupButton_Clicked;
            forgotUsernameButton.Clicked += ForgotUsernameButton_Clicked;
            forgotPassButton.Clicked += ForgotPassButton_Clicked;
        }
        // event method for forgot  password button
        async void ForgotPassButton_Clicked(object sender, EventArgs e)
        {
            // navigate to forgot password page
            await Navigation.PushAsync(new ForgotPasswordPage());
        }
        // event method for forgot  username button
        async void ForgotUsernameButton_Clicked(object sender, EventArgs e)
        {
            // navigate to forgot username page
            await Navigation.PushAsync(new ForgotUsernamePage());
        }
        // event method for sign up button button
        async void SignupButton_Clicked(object sender, EventArgs e)
        {
            // navigate to sign up/ Main page
            await Navigation.PushAsync(new MainPage());
        }
        // event method for sign in button
        async void SignInButton_Clicked(object sender, EventArgs e)
        {
            // if fields are not complete
            if (string.IsNullOrWhiteSpace(usernameEntry.Text) || string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                // display the alert
                await DisplayAlert("Fields Incomplete!", "Make sure all fields are complete before pressing sign up.", "OKAY");
                // call the clear fields method
                ClearFields();
            }
            // if the username does not exist
            else if (!users.ContainsKey(usernameEntry.Text))
            {
                // display the alert/ if the user selects sign up
                if (await DisplayAlert("Login Failed!", "Username does not exist.", "Sign Up", "Try Again"))
                {
                    // navigate to the sign up/ main page 
                    await Navigation.PushAsync(new MainPage());
                }
                // call the clear fields method
                ClearFields();
            }
            // if the password is not correct
            else if (users.ContainsKey(usernameEntry.Text) && users[usernameEntry.Text].Password != passwordEntry.Text)
            {
                // display the alert
                await DisplayAlert("Login Failed!", "Username and Password combination is not correct.", "OKAY");
                ClearFields();
            }
            // if the username and password are correct
            else
            {
                // set signed in property to true
                Application.Current.Properties["SignedIn"] = bool.TrueString;
                Application.Current.Properties["Name"] = usernameEntry.Text;
                await Application.Current.SavePropertiesAsync();
                // navigate to the homepage
                await Navigation.PushAsync(new HomePage());
                
            }
        }
        // method for clearing all fields
        private void ClearFields()
        {
            usernameEntry.Text = null;
            passwordEntry.Text = null;
        }
        // Method to read from the text file and load data 
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
                        // add the new user to the dictionary
                        users.Add(data[2], user);
                    }
                }
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BackgroundImageSource = "Gradient.png";
        }
    }
}
