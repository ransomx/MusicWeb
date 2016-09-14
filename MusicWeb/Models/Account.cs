using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace MusicWeb.Models
{
    public class Account : IdentityUser
    {
        public virtual List<Playlist> Playlists { get; set; }
    }
}