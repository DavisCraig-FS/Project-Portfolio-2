using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Alpha
{
    public partial class HomePage : TabbedPage
    {
        private ToolbarItem signOutItem;
        public HomePage()
        {
            InitializeComponent();
            // Remove the back button on the nav bar
            NavigationPage.SetHasNavigationBar(this, false);

            NavigationPage tab1View = new NavigationPage(new SearchPage());
            //tab1View.Title = "SEARCH";
            tab1View.IconImageSource = "searchwhite.png";

            FavoritesPage tab2View = new FavoritesPage();
            //tab2View.Title = "FAVORITES";
            tab2View.IconImageSource = "starwhite.png";

            Children.Add(tab1View);
            Children.Add(tab2View);

            this.signOutItem = new ToolbarItem
            {
                IconImageSource = ImageSource.FromFile("signoutwhite.png"),
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };

            this.ToolbarItems.Add(signOutItem);

            signOutItem.Clicked += SignOut_Clicked;
        }

        async void SignOut_Clicked(object sender, EventArgs e)
        {
            // display pop up when signout button is clicked. If answer is yes,
            if (await DisplayAlert("SIGNOUT", "Are you sure you would like to signout?", "YES", "CANCEL"))
            {
                // store the false value to the signedIn local storage key and navigate to root page
                Application.Current.Properties["SignedIn"] = bool.FalseString;

                await Navigation.PopToRootAsync();
            }
        }

        private  void SignOut()
        {
            
        }
    }
}
