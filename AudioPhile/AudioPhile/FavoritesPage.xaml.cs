using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AudioPhile.Models;
using Xamarin.Forms;

namespace AudioPhile
{
    public partial class FavoritesPage : INotifyPropertyChanged
    {
        // variables to hold album data and username
        List<Album> albumList = new List<Album>();
        public string name;
        public FavoritesPage()
        {
            InitializeComponent();
            // disable back button
            NavigationPage.SetHasBackButton(this, false);
            // method for loading album list
            name = (string)Application.Current.Properties["Name"];
            userLabel.Text = $"{name.ToUpper()}'s Favorites";
            // messaging center subscriptions
            MessagingCenter.Subscribe<List<Album>>(this, "UserFavorites", (sender) =>
            {
                albumList = sender;
            });
            MessagingCenter.Subscribe<Album>(this, "AddAlbum", (sender1) =>
            {
                // add sender to list
                albumList.Add(sender1);
            });
            //SerializeAlbums();
            // event
            listView.ItemSelected += ListView_ItemSelected;
            // data template for image cell
            DataTemplate dt = new DataTemplate(typeof(ImageCell));
            dt.SetValue(TextCell.TextColorProperty, Color.White);
            dt.SetValue(TextCell.DetailColorProperty, Color.White);
            dt.SetBinding(ImageCell.ImageSourceProperty, new Binding("Image"));
            dt.SetBinding(ImageCell.TextProperty, new Binding("Title"));
            dt.SetBinding(ImageCell.DetailProperty, new Binding("Artist"));
            listView.ItemTemplate = dt;
            // messaging center sender
            //MessagingCenter.Send(albumList, "UpdatedFavorites");

        }
        // method for on appearing
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //UpdateListView(listView);
            BackgroundImageSource = "Gradient.png";
            listView.ItemsSource = albumList.ToList();
        }

        // event method for list view item selected
        async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // if answer is yes, display alert
            if (await DisplayAlert("REMOVE ALBUM", "Are you sure you want to remove this album from the favorites list?", "YES", "CANCEL"))
            {
                // remove album from list
                albumList.Remove((Album)e.SelectedItem);
                // store the username and their favorites in json format
                //SerializeAlbums();
                listView.ItemsSource = albumList.ToList();
            }

        }
    }
}
