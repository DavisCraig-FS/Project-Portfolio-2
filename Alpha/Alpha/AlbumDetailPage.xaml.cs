using System;
using System.Collections.Generic;
using Alpha.Models;
using System.IO;
using Xamarin.Forms;
using System.Diagnostics;

namespace Alpha
{
    public partial class AlbumDetailPage : ContentPage
    {
        Album album = new Album();
        List<Album> albumList = new List<Album>();
        public AlbumDetailPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MessagingCenter.Subscribe<Album>(this, "AlbumDetail", (sender) =>
            {
                album = sender;
            });
            addButton.Clicked += AddButton_Clicked;

        }

        async void AddButton_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("ADD ALBUM", "Are you sure you want to add this album to your favorites?", "YES", "CANCEL"))
            {
                
                MessagingCenter.Send(album, "AddAlbum");
                /*if (!File.Exists(App.FavoriteFilePath))
                {
                    // create the file 
                    using (StreamWriter sw = File.CreateText(App.FavoriteFilePath))
                    {
                        
                        // write the album info to the file
                        sw.WriteLine($"{album.Title}|{album.Image}|{album.Artist}");
                        Debug.WriteLine($"{album.Title}|{album.Image}|{album.Artist}");
                        
                    }
                }
                // if the file does exist
                else
                {
                    // append the file
                    using (StreamWriter sw = File.AppendText(App.FavoriteFilePath))
                    {
                        
                        // write the album info to the file
                        sw.WriteLine($"{album.Title}|{album.Image}|{album.Artist}");
                        Debug.WriteLine($"{album.Title}|{album.Image}|{album.Artist}");
                        
                    }
                }*/
            }
        }
        // method for on appearing
        protected override void OnAppearing()
        {
            base.OnAppearing();
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
