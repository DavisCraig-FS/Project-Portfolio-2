using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;

namespace SignIn_Up
{
    public partial class ForgotUsernamePage : ContentPage
    {
        Dictionary<string, User> users;
        private List<User> usernames = new List<User>();
        User user;
        public ForgotUsernamePage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            users = new Dictionary<string, User>();
            ReadFromFile();
            listView.ItemsSource = usernames;
            foreach (KeyValuePair<string, User> kvp in users)
            {
                usernames.Add(kvp.Value);
            }
            
            DataTemplate dt = new DataTemplate(typeof(TextCell));
            dt.SetBinding(TextCell.TextProperty, new Binding("UserName"));
            dt.SetValue(TextCell.TextColorProperty, Color.Black);
            listView.ItemTemplate = dt;
            closeButton.Clicked += CloseButton_Clicked;
        }

        private void CloseButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = usernames.ToList();
        }
        public void ReadFromFile()
        {
            if (File.Exists(App.FilePath))
            {
                // Opening a streamreader to read all file contents
                using (StreamReader sr = new StreamReader(App.FilePath))
                {
                    // variable to hold read line (stock symbol)
                    string line;
                    // looping for every readline that exists
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] data = line.Split('|');
                        user = new User
                        {
                            FirstName = data[0],
                            LastName = data[1],
                            UserName = data[2],
                            Email = data[3],
                            Password = data[4]
                        };
                        users.Add(data[2], user);
                    }
                }
            }
        }
    }
}
