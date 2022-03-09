using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;
using APIConnection.Models;

namespace APIConnection
{
    public partial class MainPage : ContentPage
    {
        Album album;
        public MainPage()
        {
            InitializeComponent();
            album = new Album();
            connectButton.Clicked += ConnectButton_Clicked;
        }

        async void ConnectButton_Clicked(object sender, EventArgs e)
        {
            DataManager dm = new DataManager();
            album = await dm.GetAlbum();
            albumLabel.Text = album.Name;
        }
    }
}
