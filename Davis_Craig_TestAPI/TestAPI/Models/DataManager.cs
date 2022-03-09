using System;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace APITest.Models
{
    public class DataManager
    {

        
        WebClient apiConnection = new WebClient();
        string artist { get; set; }
        string apiKey = "&api_key=aa18478028ace5e6ecdba60afafb2dbf&format=json";
        string startAPI = $"http://ws.audioscrobbler.com/2.0/?method=artist.gettopalbums&artist=";
        

        string apiEndPoint
        {
            get
            {
                return startAPI + artist + apiKey;
            }
        }
        List<Album> albumList = new List<Album>();
        
        public DataManager(string artistToSearch)
        {
            artist = artistToSearch;
        }


        public async Task<List<Album>> GetAlbums()
        {
            string apiString = await apiConnection.DownloadStringTaskAsync(apiEndPoint);
            JObject jsonData = JObject.Parse(apiString);
            JObject topAlbums = (JObject)jsonData["topalbums"];
            JArray albumArray = (JArray)topAlbums["album"];

            foreach(var a in albumArray)
            {
                albumList.Add(new Album
                {
                    Title = a["name"].ToString(),
                    
                });
            }

                    
                    

            return albumList;
        }
    }
}
