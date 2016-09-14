using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicWeb.Models
{
    public class ChoosePlaylist
    {
        [Required]
        public int SongId { get; set; }
        [Required]
        public string SearchedSong { get; set; }
    }
}