using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicWeb.Models
{
    public class Playlist
    {
        public virtual int Id { get; set; }
        public virtual List<Song> SongList { get; set; }

        [Required(ErrorMessage = "Your {0} is required!")]
        public virtual string Name { get; set; }
        public virtual string Creator_id { get; set; }
        public virtual string Creator_name { get; set; }
    }
}