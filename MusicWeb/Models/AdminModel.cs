using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicWeb.Models
{
    public class AdminModel
    {
        public List<Account> Users { get; set; }
        public List<Song> Songs { get; set; }
        public List<Playlist> Playlists { get; set; }

        //Traffic information
        public int NoDownloads { get; set; }
        public int NoUploads { get; set; }
    }
}