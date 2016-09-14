using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MusicWeb
{
    public class MusicDBInitializer : System.Data.Entity.DropCreateDatabaseAlways<MusicWebDB>
    {
        protected override void Seed(MusicWebDB context)
        {
            /* Adding genres */
            Genre g = new Genre { Name = "Electro" };
            context.Genres.Add(g);
            context.Genres.Add(new Genre { Name = "Dubstep" });
            context.Genres.Add(new Genre { Name = "Hip hop" });
            context.Genres.Add(new Genre { Name = "Rock" });
            context.Genres.Add(new Genre { Name = "Funk" });
            context.Genres.Add(new Genre { Name = "Punk" });
            context.Genres.Add(new Genre { Name = "Metal" });
            context.Genres.Add(new Genre { Name = "Pop" });
            context.Genres.Add(new Genre { Name = "Reggae" });

            /* Adding artists */
            Artist artist = new Artist { Name = "Daft Punk" };

            /* Adding songs */
            Song s0 = new Song
            {
                Title = "Recognizer",
                Artist = artist,
                Genre = g,
                Filename = "04 - Recognizer.mp3",
                TimeUploaded = DateTime.Now
            };
            Song s1 = new Song
            {
                Title = "Armory",
                Artist = artist,
                Genre = g,
                Filename = "05 - Armory.mp3",
                TimeUploaded = DateTime.Now
            };
            Song s2 = new Song
            {
                Title = "Arena",
                Artist = artist,
                Genre = g,
                Filename = "06 - Arena.mp3",
                TimeUploaded = DateTime.Now
            };
            Song s3 = new Song
            {
                Title = "Rinzler",
                Artist = artist,
                Genre = g,
                Filename = "07 - Rinzler.mp3",
                TimeUploaded = DateTime.Now
            };
            Song s4 = new Song
            {
                Title = "Derezzed",
                Artist = artist,
                Genre = g,
                Filename = "13 - Derezzed.mp3",
                TimeUploaded = DateTime.Now
            };
            Song s5 = new Song
            {
                Title = "Disc Wars",
                Artist = artist,
                Genre = g,
                Filename = "17 - Disc Wars.mp3",
                TimeUploaded = DateTime.Now
            };
            Song s6 = new Song
            {
                Title = "Tron Legacy(End Titles)",
                Artist = artist,
                Genre = g,
                Filename = "21 - TRON Legacy (End Titles).mp3",
                TimeUploaded = DateTime.Now
            };
            Song s7 = new Song
            {
                Title = "End Of Line",
                Artist = artist,
                Genre = g,
                Filename = "12 - End of Line.mp3",
                TimeUploaded = DateTime.Now
            };
            Song s8 = new Song
            {
                Title = "Son of Flynn",
                Artist = artist,
                Genre = g,
                Filename = "03 - The Son of Flynn.mp3",
                TimeUploaded = DateTime.Now
            };
            Song s9 = new Song
            {
                Title = "Overtorture",
                Artist = artist,
                Genre = g,
                Filename = "01 - Overture.mp3",
                TimeUploaded = DateTime.Now
            };
            context.Songs.Add(s0);
            context.Songs.Add(s1);
            context.Songs.Add(s2);
            context.Songs.Add(s3);
            context.Songs.Add(s4);
            context.Songs.Add(s5);
            context.Songs.Add(s6);
            context.Songs.Add(s7);
            context.Songs.Add(s8);
            context.Songs.Add(s9);
            Song[] songs = { s0, s1, s2, s3, s4, s5, s6, s7, s8, s9 };
            /* Adding playlists */

            /*    
            Account acc = new Account()
            {
                Username = "admin",
                Password = "admin",
                Email = "xransomx@gmail.com",
                Playlists = new List<Playlist>()
            };*/

            /* Adding users 
            UserManager<Account> userManager;
            RoleManager<AccountRole> roleManager;
            MusicWebDB db = new MusicWebDB();

            UserStore<Account> userStore = new UserStore<Account>(db);
            userManager = new UserManager<Account>(userStore);

            RoleStore<AccountRole> roleStore = new RoleStore<AccountRole>(db);
            roleManager = new RoleManager<AccountRole>(roleStore);

            Account user = new Account();

            user.UserName = "admin";
            user.Email = "practicaltester@gmail.com";
            string password = "123456789"; //A cool one..

            IdentityResult result = userManager.Create(user, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, "RegisteredUser");
            }
            */
            Playlist p = new Playlist()
            {
                Name = "Tron - Legacy OST",
                Creator = null,
                SongList = new List<Song>(songs)
            };

            //acc.Playlists.Add(p);
            //context.Accounts.Add(acc);
            context.Playlists.Add(p);

            base.Seed(context);
        }
    }
}