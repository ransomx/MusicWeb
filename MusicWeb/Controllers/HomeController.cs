using MusicWeb.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using System.IO;
using System.Net;
using System.Security.Claims;

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
            else prev = CurrentSong;
            return prev;
        }

        private Song GetSong(int index)
        {
            Song ret = null;
            if (index >= 0 && index < CurrentPlaylist.SongList.Count)
            ret = CurrentPlaylist.SongList.ElementAt(index);

            return ret;
        }

        private Song GetNextSong()
        {
            Song next = null;
            if (CurrentPlaylist != null)
                next = GetSong(++SongIndex);
            CurrentSong = next;
            return next;
        }

        // GET Index
        public ActionResult Index()
        {
            return View();
        }

        // GET Error
        public ActionResult Error(string message)
        {
            ViewBag.error = message;
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
            var principal = ClaimsPrincipal.Current;

         // Look for the groups claim for the 'Dev/Test' group.
         Claim role = principal.FindFirst("extension_Role");

            if ( role != null && role.Value.Contains("Admin"))
            {
                return PartialView("../Shared/_AdminUserMenu");
            }
            else if(role != null && role.Value.Equals("User"))
            {
                return PartialView("../Shared/_UserMenu");
            }
            else
            {
                return PartialView("../Shared/_NoUserMenu");
            }
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

        public FileResult Download(string toDownload)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("media");

            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(toDownload);

            using (WebClient wc = new WebClient())
            {
                var byteArr = wc.DownloadData(blockBlob.Uri);
                return File(byteArr, "application/zip", toDownload);
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
                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionString"));

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference("media");

                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(CurrentSong.Filename);
                return blockBlob.Uri.ToString();
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