using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicWeb;
using MusicWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MusicWeb.Controllers
{
    public class PlaylistController : Controller
    {
        private MusicWebDB db = new MusicWebDB();

        // GET: Playlists
        public ActionResult Index()
        {
            return PartialView(db.Playlists.ToList());
        }

        // GET: Playlists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            playlist.SongList.Count();
            return PartialView(playlist);
        }

        // GET: Playlists/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Playlists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                /*
                var store = new UserStore<Account>(db);
                var userManager = new UserManager<Account>(store);


                var claimsIdentity = User.Identity as ClaimsIdentity;
                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                var userIdValue = userIdClaim.Value;

                userManager.FindById(userIdValue).Playlists.Add(playlist);
                store.Context.SaveChanges();
                return PartialView("../Home/HomeSearch");*/
            }
            return PartialView(playlist);
        }

        // GET: Playlists/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            return PartialView(playlist);
        }

        // POST: Playlists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Playlist playlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView(playlist);
        }

        // GET: Playlists/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Playlist playlist = db.Playlists.Find(id);
            if (playlist == null)
            {
                return HttpNotFound();
            }
            return PartialView(playlist);
        }

        // POST: Playlists/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Playlist playlist = db.Playlists.Find(id);
            db.Entry(playlist).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("../Home/HomeSearch");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize]
        public ActionResult RemoveSong(string filename, int playlistId)
        {
            Playlist toUpdate = (from play in db.Playlists
                           where play.Id == playlistId
                           select play).FirstOrDefault();
            Song toRemove = (from song in db.Songs
                           where song.Filename == filename
                           select song).FirstOrDefault();
            toUpdate.SongList.Remove(toRemove);
            db.SaveChanges();
            return PartialView("Details", toUpdate);
        }

        [Authorize]
        [HttpPost]
        public void AddToPlaylist(ChoosePlaylist topass, int PlaylistId)
        {
            Song s = db.Songs.FirstOrDefault(m => m.Id == topass.SongId);
            db.Playlists.FirstOrDefault(p => p.Id == PlaylistId).SongList.Add(s);
            db.SaveChanges();

            ViewBag.Message = s.Title + " added to " + db.Playlists.FirstOrDefault(p => p.Id == PlaylistId).Name;

            Search model = new Search();
            Song song = new Song { Title = topass.SearchedSong };
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChoosePlaylist(int toAdd, string searchedSong)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var userIdValue = userIdClaim.Value;
            Account acc = db.Users.FirstOrDefault(a => a.Id == userIdValue);
            ChoosePlaylist topass = new ChoosePlaylist();

            topass.SongId = toAdd;
            topass.SearchedSong = searchedSong;
            ViewBag.PlaylistId = new SelectList(acc.Playlists, "Id", "Name");

            return PartialView("ChoosePlaylist", topass);
        }

        [Authorize]
        public ActionResult ListPlaylists()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var userIdValue = userIdClaim.Value;

            IEnumerable<Playlist> playlists = (from s in db.Playlists
                                               where s.Creator.Id == userIdValue
                                               select s).ToList();
            return PartialView(playlists);
        }
    }
}
