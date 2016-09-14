using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicWeb.Models
{
    public class Song
    {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }
        public virtual Artist Artist { get; set; }
        public virtual Genre Genre { get; set; }

        public virtual string Filename { get; set; }
        public virtual DateTime TimeUploaded { get; set; }

        public virtual List<Playlist> Playlists { get; set; }

        public Song()
        {

        }

        public Song(int id, string title, Artist artist)
        {
            Id = id; Title = title; Artist = artist;
        }
    }
}