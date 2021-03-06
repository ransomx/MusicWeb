﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MusicWeb.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MusicWeb
{
    public class MusicWebDB : DbContext
    {
        public MusicWebDB() : base("name=MusicWebDBAzure") { }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}