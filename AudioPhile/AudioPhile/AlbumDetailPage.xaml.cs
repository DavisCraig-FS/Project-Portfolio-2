using System;
using System.Collections.Generic;
using AudioPhile.Models;
using Xamarin.Forms;

namespace AudioPhile
{
    public partial class AlbumDetailPage : ContentPage
    {
        Album album = new Album();
        public AlbumDetailPage()
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Subscribe<Album>(this, "AlbumDetail", (sender) =>
            {
                album = sender;
            });
            // event
            addButton.Clicked += AddButton_Clicked;
        }
        // event for add button
        async void AddButton_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("ADD ALBUM", "Are you sure you want to add this album to your favorites?", "YES", "CANCEL"))
            {
                MessagingCenter.Send(album, "AddAlbum");
            }
        }
        // method for on appearing
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BackgroundImageSource = "Gradient.png";
            // variables to hold the binding context
            var image = BindingContext as Album;
            var artist = BindingContext as Album;
            var title = BindingContext as Album;
            // assign binded values to diplay
            aristLabel.Text = artist.Artist;
            albumImage.Source = image.Image;
            albumLabel.Text = title.Title;
        }
    }
}
