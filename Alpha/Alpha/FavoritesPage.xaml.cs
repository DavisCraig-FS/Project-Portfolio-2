using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Alpha.Models;
using Xamarin.Forms;
using System.Diagnostics;

namespace Alpha
{
    public partial class FavoritesPage : ContentPage
    {
        List<Album> albumList = new List<Album>();
        Album album = new Album();
        public FavoritesPage()
        {
            InitializeComponent();
            // Remove the back button on the nav bar
            NavigationPage.SetHasNavigationBar(this, false);

            ReadFromFile();
            Debug.WriteLine(albumList.Count);
            
            listView.ItemSelected += ListView_ItemSelected;

            DataTemplate dt = new DataTemplate(typeof(ImageCell));
            dt.SetBinding(ImageCell.ImageSourceProperty, new Binding("Image"));
            dt.SetBinding(ImageCell.TextProperty, new Binding("Title"));
            dt.SetBinding(ImageCell.DetailProperty, new Binding("Artist"));
            listView.ItemTemplate = dt;
            
        }

        async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (await DisplayAlert("REMOVE ALBUM", "Are you sure you want to remove this album from the favorites list?", "YES", "CANCEL"))
            {
                albumList.Remove(e.SelectedItem as Album);
            }
        }
        public void ReadFromFile()
        {
            if (File.Exists(App.FavoriteFilePath))
            {
                // Opening a streamreader to read all file contents
                using (StreamReader sr = new StreamReader(App.FavoriteFilePath))
                {
                    // variable to hold read line (stock symbol)
                    string line;
                    // looping for every readline that exists
                    while ((line = sr.ReadLine()) != null)
                    {
                        // create an array and add data
                        string[] data = line.Split('|');
                        // instantiate a new user object and add all info
                        album = new Album
                        {
                            Title = data[0],
                            Image = data[1],
                            Artist = data[2]
                        };
                        albumList.Add(album);
                    }
                }

            }
            Debug.WriteLine(albumList.ToList());
            listView.ItemsSource = albumList.ToList();
        }
    }
}
