using System;
using System.Collections.Generic;

using Xamarin.Forms;

// Craig Davis
// C202203
// MDV228
// 2.1 CODE: Sign-In/ Sign-Up

namespace SignIn_Up
{
    public partial class HomePage : ContentPage
    {
        
        public HomePage()
        {
            InitializeComponent();
            // remove the back button from the nav bar
            NavigationPage.SetHasNavigationBar(this, false);
            // subscribe to data via messaging center
            MessagingCenter.Subscribe<String>(this, "DisplayUser", (sender) =>
            {
                // assign the the sender's value to the userlabel
                userLabel.Text = sender.ToUpper();
            });
            // events
            signOutButton.Clicked += SignOutButton_Clicked;
        }
        // event method for the sign out button
        async void SignOutButton_Clicked(object sender, EventArgs e)
        {
            // assign the user label value to null
            userLabel.Text = null;
            // navigate to the sign up/ main page
            await Navigation.PushAsync(new MainPage());
        }
    }
}
