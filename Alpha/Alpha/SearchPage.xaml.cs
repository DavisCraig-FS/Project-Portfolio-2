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
            // disable back button
            NavigationPage.SetHasBackButton(this, false);
            searchButton.Clicked += SearchButton_Clicked;
            listView.ItemsSource = albumList;
            listView.ItemSelected += ListView_ItemSelected;
            string userName = (string)Application.Current.Properties["Name"];
            userLabel.Text = userName.ToUpper();
            DataTemplate dt = new DataTemplate(typeof(ImageCell));
            dt.SetValue(TextCell.TextColorProperty, Color.White);
            dt.SetValue(TextCell.DetailColorProperty, Color.White);
            dt.SetBinding(ImageCell.ImageSourceProperty, new Binding("Image"));
            dt.SetBinding(ImageCell.TextProperty, new Binding("Title"));
            dt.SetBinding(ImageCell.DetailProperty, new Binding("Artist"));
            listView.ItemTemplate = dt;
        }
        // event method for list item selected
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // if the item selected is not null
            if (e.SelectedItem != null)
            {
                // navigate to album detail page 
                Navigation.PushAsync(new AlbumDetailPage
                {
                    // and bind the selected item's context
                    BindingContext = e.SelectedItem as Album
                });
                // send the selected album via messaging center
                MessagingCenter.Send<Album>((Album)e.SelectedItem, "AlbumDetail");
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BackgroundImageSource = "Gradient.png";
        }
        // event method for search button
        async void SearchButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryLabel.Text))
            {
                await DisplayAlert("Fields Incomplete!", "Make sure the entry field is not empty before pressing search.", "OKAY");
            }
            else
            {
                // call method for API call
                ArtistAlbumSearch();
            }
            
        }
        // method for API call 
        async void ArtistAlbumSearch()
        {
            // instantiate datamanager and assign values to albumList
            DataManager dm = new DataManager(entryLabel.Text.ToLower());
            albumList = await dm.GetAlbums();
            if(albumList == null)
            {
                await DisplayAlert("Error!", "The search returned no results. Please make sure the artist is typed correctly.", "OKAY");
            }
            else
            {
                listView.ItemsSource = albumList.ToList();
            }
        }
    }
        
}
