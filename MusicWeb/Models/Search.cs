using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicWeb.Models
{
    public class Search
    {
        public Song SearchedSong { get; set; }
        public IEnumerable<Song> SearchResult { get; set; }

        [Required, FileExtensions(ErrorMessage = "Specify an audio file.", Extensions = "mp3,wav,ogg")]
        public HttpPostedFileBase File { get; set; }

        public Account Logged { get; set; }

        public Song songToPlay { get; set; }
    }
}