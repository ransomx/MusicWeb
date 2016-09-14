﻿using MusicWeb.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicWeb.Controllers
{
    public class HomeController : Controller
    {
        private MusicWebDB db = new MusicWebDB();

        public void LoadPlaylist(Playlist p)
        {
            CurrentPlaylist = p;
            CurrentSong = p.SongList.FirstOrDefault();
            Session["SongIndex"] = 0;
        }

        private Playlist CurrentPlaylist
        {
            get { return Session["CurrentPlaylist"] as Playlist; }
            set { Session["CurrentPlaylist"] = value; }
        }

        private Song CurrentSong
        {
            get { return Session["CurrentSong"] as Song; }
            set { Session["CurrentSong"] = value; }
        }

        private int SongIndex
        {
            get {
                if(Session["SongIndex"] !=null)
                    return Convert.ToInt32(Session["SongIndex"]);
                return 9999;
            }
            set { Session["SongIndex"] = value; }
        }

        private Song GetPrevSong()
        {
            Song prev = null;
            if (CurrentPlaylist != null)
                prev = GetSong(--SongIndex);
            return prev;
        }

        private Song GetSong(int index)
        {
            Song ret = null;
            if (index < 0) SongIndex = index = CurrentPlaylist.SongList.Count - 1;
            else if (index >= CurrentPlaylist.SongList.Count)
                SongIndex = index = 0;
            ret = CurrentPlaylist.SongList.ElementAt(index);
            return ret;
        }

        private Song GetNextSong()
        {
            Song next = null;
            if (CurrentPlaylist !=null)
                next = GetSong(++SongIndex);
            return next;
        }

        // GET Index
        public ActionResult Index()
        {
            return View();
        }

        // GET _UserControlPanel
        public ActionResult UserControlPanel()
        {
            return PartialView("../Shared/_UserControlPanel");
        }

        // GET _UserMenu
        public ActionResult UserMenu()
        {
            return PartialView("../Shared/_UserMenu");
        }

        // GET About
        public ActionResult About()
        {
            return PartialView();
        }

        // GET HomeSearch
        public ActionResult HomeSearch()
        {
            return PartialView();
        }

        // GET Contact
        public ActionResult Contact()
        {
            return PartialView();
        }

        public ActionResult Download(string songToDownload)
        {
            try
            {
                var fs = System.IO.File.OpenRead(Server.MapPath("~/Content/Media/uploads/" + songToDownload));
                return File(fs, "application/zip", songToDownload);
            }
            catch
            {
                this.HttpContext.AddError(new HttpException(404, "Song file not found: " + songToDownload));
                return View();
            }
        }

        [HttpGet]
        public ActionResult Search(Search model)
        {
            if (model.SearchedSong == null)
                model.SearchedSong = (Song)TempData["prevSearch"];

            var songs = (from s in db.Songs
                         where s.Title.Contains(model.SearchedSong.Title)
                         select s).ToList();

            model.SearchedSong = model.SearchedSong;
            model.SearchResult = songs;
            return PartialView(model);
        }

        /* Gets a path to playsong for audio player src
          Example: ~/Content/Media/uploads/song.mp3
         */
        [HttpGet]
        [AllowAnonymous]
        public string PlaySong()
        {
            if (CurrentSong != null)
            {
                var filename = CurrentSong.Filename;
                return filename;
            }
            return "Nothing to play here";
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult PlaySpecific(int songId)
        {
            Song songToPlay = db.Songs.FirstOrDefault(s => s.Id == songId);
            if (songToPlay != null)
            {
                CurrentSong = songToPlay;
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        /* This method sets the playlist depending on it's id */
        [AllowAnonymous]
        public JsonResult PlayAll(int playlistId)
        {
            Playlist toLoad = db.Playlists.FirstOrDefault(
                p => p.Id == playlistId);
            if (toLoad != null)
            {
                LoadPlaylist(toLoad);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else return Json(false, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public void PlayNext()
        {
            if (CurrentSong != null)
            {
                CurrentSong = GetNextSong();
            }
        }

        [AllowAnonymous]
        public void PlayPrev()
        {
            if (CurrentSong != null)
            {
                CurrentSong = GetPrevSong();
            }
        }
    }
}