using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Alpha.Models;
using Newtonsoft.Json.Linq;

namespace Alpha
{
    public partial class SearchPage : ContentPage
    {
        List<Album> albumList = new List<Album>();

        public SearchPage()
        {
            InitializeComponent();
            // Remove the back button on the nav bar
            NavigationPage.SetHasNavigationBar(this, false);
            searchButton.Clicked += SearchButton_Clicked;
            listView.ItemsSource = albumList;
            listView.ItemSelected += ListView_ItemSelected;

            DataTemplate dt = new DataTemplate(typeof(ImageCell));
            dt.SetBinding(ImageCell.ImageSourceProperty, new Binding("Image"));
            dt.SetBinding(ImageCell.TextProperty, new Binding("Title"));
            dt.SetBinding(ImageCell.DetailProperty, new Binding("Artist"));
            listView.ItemTemplate = dt;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Navigation.PushAsync(new AlbumDetailPage
                {
                    BindingContext = e.SelectedItem as Album
                });
            }
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            ArtistAlbumSearch();
        }
        async void ArtistAlbumSearch()
        {
            DataManager dm = new DataManager(entryLabel.Text.ToLower());
            albumList = await dm.GetAlbums();
            listView.ItemsSource = albumList.ToList();

        }
    }
        
}
