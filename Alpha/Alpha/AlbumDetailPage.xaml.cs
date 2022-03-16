using System;
using System.Collections.Generic;
using Alpha.Models;
using Xamarin.Forms;

namespace Alpha
{
    public partial class AlbumDetailPage : ContentPage
    {
        Album album = new Album();

        public AlbumDetailPage()
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
