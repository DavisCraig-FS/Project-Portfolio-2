using System;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace APIConnection.Models
{
    public class DataManager
    {
        WebClient apiConnection = new WebClient();
        string artist { get; set; }
        string apiURL = "http://ws.audioscrobbler.com/2.0/?method=artist.gettopalbums&artist=johnlegend&api_key=aa18478028ace5e6ecdba60afafb2dbf&format=json";
        Album album = new Album();
        
        public DataManager()
        {
            
        }
        public async Task<Album> GetAlbum()
        {
            string apiString = await apiConnection.DownloadStringTaskAsync(apiURL);
            JObject jsonData = JObject.Parse(apiString);
            Debug.WriteLine(jsonData.ToString());
            JObject topAlbums = (JObject)jsonData["topalbums"];
            JArray albumArray = (JArray)topAlbums["album"];
            
            album.Name = albumArray[0]["name"].ToString();

            return album;
        }
    }
}
