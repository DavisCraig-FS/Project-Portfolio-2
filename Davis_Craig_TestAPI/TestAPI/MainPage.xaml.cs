using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APITest.Models;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace TestAPI
{
    public partial class MainPage : ContentPage
    {
        List<Album> albumList = new List<Album>();
        
        public MainPage()
        {
            InitializeComponent();
            
            searchButton.Clicked += SearchButton_Clicked;
            listView.ItemsSource = albumList;

            DataTemplate dt = new DataTemplate(typeof(ImageCell));

            dt.SetBinding(ImageCell.ImageSourceProperty, new Binding("Image"));
            dt.SetBinding(ImageCell.TextProperty, new Binding("Title"));
            listView.ItemTemplate = dt;
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
