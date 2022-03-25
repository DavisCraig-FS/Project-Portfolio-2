using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Alpha
{
    public partial class HomePage : INotifyPropertyChanged
    {
        private ToolbarItem signOutItem;
        public string name;
        List<Album> albumList = new List<Album>();

        public HomePage()
        {
            InitializeComponent();
            var c = Color.FromHex("#000000");
            this.BarBackgroundColor = c;
            
            // Remove the back button on the nav bar
            NavigationPage.SetHasNavigationBar(this, false); 
            NavigationPage tab1View = new NavigationPage(new SearchPage());
            tab1View.IconImageSource = "searchwhite.png";
            
            FavoritesPage tab2View = new FavoritesPage();
            tab2View.IconImageSource = "starwhite.png";
            // add tab page to homepage
            Children.Add(tab1View);
            Children.Add(tab2View);
            // assign values to signout toolbar item
            signOutItem = new ToolbarItem
            {
                IconImageSource = ImageSource.FromFile("signoutwhite.png"),
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            // add signout item to toolbar
            ToolbarItems.Add(signOutItem);
            // event
            signOutItem.Clicked += SignOut_Clicked;
            // variable to hold value retrieved from current properties
            name = (string)Application.Current.Properties["Name"];
            // deserialize list object into json data to store in current properties
            if (Application.Current.Properties.ContainsKey($"{name}"))
            {
                albumList = JsonConvert.DeserializeObject<List<Album>>((string)Application.Current.Properties[$"{name}"]);
            }
            MessagingCenter.Send(albumList, "UserFavorites");
            // save current property data
            Application.Current.SavePropertiesAsync();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            BackgroundImageSource = "Gradient.png";
        }
        // event method for signout button
        async void SignOut_Clicked(object sender, EventArgs e)
        {
            // display pop up when signout button is clicked. If answer is yes,
            if (await DisplayAlert("SIGNOUT", "Are you sure you would like to signout?", "YES", "CANCEL"))
            {
                // messaging center subscription
                MessagingCenter.Subscribe<List<Album>>(this, "UpdatedFavorites", (sender1) =>
                {
                    albumList = sender1;
                });
                // store the username and their favorites in json format
                var jsonValueToSave = JsonConvert.SerializeObject(albumList);
                Application.Current.Properties[$"{name}"] = jsonValueToSave;
                Application.Current.Properties["Name"] = null;
                // store the false value to the signedIn local storage key and navigate to root page
                Application.Current.Properties["SignedIn"] = bool.FalseString;
                await Application.Current.SavePropertiesAsync();
                await Navigation.PushAsync(new MainPage());
            }
        }

    }
}
