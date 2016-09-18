using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicWeb.Models
{
    public class AdminModel
    {
        public List<Song> Songs { get; set; }
        public List<Playlist> Playlists { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Artist> Artists { get; set; }

        //Traffic information
        public int NoDownloads { get; set; }
        public int NoUploads { get; set; }
    }
}