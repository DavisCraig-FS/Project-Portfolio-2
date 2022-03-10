using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SignIn_Up
{
    public partial class HomePage : ContentPage
    {
        
        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Subscribe<String>(this, "DisplayUser", (sender) =>
            {
                userLabel.Text = sender.ToUpper();
            });

            signOutButton.Clicked += SignOutButton_Clicked;
        }

        async void SignOutButton_Clicked(object sender, EventArgs e)
        {
            userLabel.Text = null;
            await Navigation.PushAsync(new MainPage());
        }
    }
}
