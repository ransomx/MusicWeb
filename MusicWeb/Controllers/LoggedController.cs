using MusicWeb.Models;
using System;
using System.IO;
using System.Web.Mvc;

namespace MusicWeb.Controllers
{
    public class LoggedController : Controller
    {
        MusicWebDB db = new MusicWebDB();

        // GET: Logged/Upload
        [Authorize]
        public ActionResult Upload()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name");
            return PartialView();
        }

        // GET: Logged/UploadFile
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFile(Upload model, int GenreId)
        {
            if (model.UploadFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(model.UploadFile.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Media/uploads/"), fileName);
                model.UploadFile.SaveAs(path);

                Song toAdd = new Song() {
                    Artist = model.Artist,
                    Title = model.Title,
                    Filename = fileName,
                    TimeUploaded = DateTime.Now,
                    Genre = db.Genres.Find(GenreId)
                };
                db.Songs.Add(toAdd);
                db.SaveChanges();
            }
            return View("../Home/Index");
        }
    }
}