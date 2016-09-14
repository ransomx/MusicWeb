using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicWeb.Models
{
    public class Upload
    {
        [Required, FileExtensions(ErrorMessage = "Specify an audio file.", Extensions = "mp3,wav,ogg")]
        public HttpPostedFileBase UploadFile { get; set; }
        [Required(ErrorMessage = "Your {0} is required!")]
        public string Title { get; set; }
        public Artist Artist { get; set; }
        
        public Genre Genre { get; set; }

        public Genre GenreList { get; set; }
    }
}