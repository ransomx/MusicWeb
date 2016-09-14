using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicWeb.Models
{
    public class Genre
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Your {0} is required!")]
        public virtual string Name { get; set; }
    }
}