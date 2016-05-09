using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BlobTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {


            return View();
        }

        public ActionResult UploadImages(HttpPostedFileBase image)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount
                .Parse("DefaultEndpointsProtocol=https;AccountName=tiytest;AccountKey=SQupPZ26y+CfChJFfzN2fY9F85eejm4oiO1H4G9yvePBIpSO1ByVm3Vb1pHE7blDqYpHvrUvGk8jeDPkMWsOOw==");

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("test");

            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });


            CloudBlockBlob blockBlob = container.GetBlockBlobReference($"11-thumb.jpg");
            blockBlob.UploadFromStream(image.InputStream);


            return RedirectToAction("Index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}