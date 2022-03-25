using System;
using System.Collections.Generic;

namespace AudioPhile.Models
{
    public class Album
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }

        public class Albums
        {
            public static List<Album> Get()
            {
                return new List<Album>();
            }
        }
    }
    }
}
