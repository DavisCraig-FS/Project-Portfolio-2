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
    public partial class ForgotPasswordPage : ContentPage
    {
        User user;
        Dictionary<string, User> users = new Dictionary<string, User>();
        public ForgotPasswordPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            ReadFromFile();
            forgotPassButton.Clicked += ForgotPassButton_Clicked;
            closeButton.Clicked += CloseButton_Clicked;
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        async void ForgotPassButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(forgotUsernameEntry.Text))
            {
                await DisplayAlert("Field Incomplete!", "Please type a valid username.", "OKAY");
            }
            else if (!users.ContainsKey(forgotUsernameEntry.Text))
            {
                if (await DisplayAlert("Invalid Entry!", "Username does not exist.", "Sign Up", "Try Again"))
                {
                    await Navigation.PushAsync(new MainPage());
                }
            }
            else
            { 
                passwordLabel.Text = users[forgotUsernameEntry.Text].Password;
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
    }
}
