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
        List<string> imageList = new List<string>();

        public MainPage()
        {
            InitializeComponent();
            
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
            //MessagingCenter.Send((Album)e.SelectedItem, "Image");
            //Navigation.PushAsync(new DisplayAlbumPage());
            if(e.SelectedItem != null)
            {
                Navigation.PushAsync(new DisplayAlbumPage
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
