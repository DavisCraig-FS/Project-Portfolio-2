using System;
using System.Collections.Generic;
using APITest.Models;
using Xamarin.Forms;

namespace TestAPI
{
    public partial class DisplayAlbumPage : ContentPage
    {
        Album album = new Album();
        public DisplayAlbumPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<Album>(this, "Image", (sender) =>
            {
                album = sender;
            });
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var image = BindingContext as Album;
            var artist = BindingContext as Album;
            var title = BindingContext as Album;

            aristLabel.Text = artist.Artist;
            albumImage.Source = image.Image;
            albumLabel.Text = title.Title;
        }
    }
}
