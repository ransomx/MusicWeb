using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
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
                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference("media");

                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(model.UploadFile.FileName);

                // Create or overwrite the "myblob" blob with contents from a local file.
                blockBlob.UploadFromStream(model.UploadFile.InputStream);

                Song toAdd = new Song() {
                    Artist = model.Artist,
                    Title = model.Title,
                    Filename = model.UploadFile.FileName,
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