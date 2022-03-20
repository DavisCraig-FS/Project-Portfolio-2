using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Alpha.Models;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.ObjectModel;
using static Alpha.Models.Album;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace Alpha
{
    public partial class FavoritesPage : INotifyPropertyChanged
    {
        
        List<Album> albumList = new List<Album>();
        Album album = new Album();
        public string name;
        public FavoritesPage()
        {
            InitializeComponent();
            // Remove the back button on the nav bar
            NavigationPage.SetHasNavigationBar(this, false);
            //ReadFromFile();
            LoadAlbumList();
            
            
            MessagingCenter.Subscribe<Album>(this, "AddAlbum", (sender) =>
            {
                albumList.Add(sender);
                Debug.WriteLine(sender.Title);
            });
            MessagingCenter.Send(albumList, "UserFavorites");
            //listView.ItemsSource = albumList.ToList();
            //UpdateListView(listView);
            listView.ItemSelected += ListView_ItemSelected;

            DataTemplate dt = new DataTemplate(typeof(ImageCell));
            dt.SetBinding(ImageCell.ImageSourceProperty, new Binding("Image"));
            dt.SetBinding(ImageCell.TextProperty, new Binding("Title"));
            dt.SetBinding(ImageCell.DetailProperty, new Binding("Artist"));
            listView.ItemTemplate = dt;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //ReadFromFile();
            UpdateListView(listView);

        }
        async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (await DisplayAlert("REMOVE ALBUM", "Are you sure you want to remove this album from the favorites list?", "YES", "CANCEL"))
            {
                albumList.Remove((Album)e.SelectedItem);
                // store the username and their favorites in json format
                SerializeAlbums();
                /*File.Delete(App.FavoriteFilePath);
                if (!File.Exists(App.FavoriteFilePath))
                {
                    using (StreamWriter sw = File.CreateText(App.FavoriteFilePath))
                    {
                        foreach(Album a in albumList)
                        {
                            sw.WriteLine($"{album.Title}|{album.Image}|{album.Artist}");
                        }
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(App.FavoriteFilePath))
                    {
                        foreach (Album a in albumList)
                        {
                            sw.WriteLine($"{album.Title}|{album.Image}|{album.Artist}");
                        }
                    }
                }*/
                listView.ItemsSource = albumList.ToList();
            }
        }
        public void ReadFromFile()
        {
            if (File.Exists(App.FavoriteFilePath))
            {
                // Opening a streamreader to read all file contents
                using (StreamReader sr = new StreamReader(App.FavoriteFilePath))
                {
                    // variable to hold read line 
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
                        if (!albumList.Contains(album))
                        {
                            albumList.Add(album);
                        }
                        
                    }
                }
            }
        }
        void LoadAlbumList()
        {
            name = (string)Application.Current.Properties["Name"];
            if (Application.Current.Properties.ContainsKey("List"))
            {
                albumList = JsonConvert.DeserializeObject<List<Album>>((string)Application.Current.Properties[$"{name}"]);
            }
            
        }
        async void SerializeAlbums()
        {
            var jsonValueToSave = JsonConvert.SerializeObject(albumList);
            Application.Current.Properties["List"] = jsonValueToSave;
            await Application.Current.SavePropertiesAsync();
        }
        void UpdateListView(ListView listView)
        {
            var itemsSource = albumList.ToList();
            listView.ItemsSource = null;
            listView.ItemsSource = itemsSource;
        }
    }
}
