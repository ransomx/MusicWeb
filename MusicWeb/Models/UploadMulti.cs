using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicWeb.Models
{
    public class UploadMulti
    {
        public List<Upload> Uploads { get; set; }
        public Genre GenreList { get; set; }
    }
}