using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Web;
using EPiServer.Web.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EPiServer.CJansson.MediaDataVersionRouter.Controllers
{
    public class MediaDataVersionController : Controller, IRenderTemplate<ContentVersion>
    {
        private IContentLoader contentLoader;

        public MediaDataVersionController(IContentLoader contentLoader)
        {
            this.contentLoader = contentLoader;
        }

        public ActionResult Index()
        {
            ContentVersion contentVersion = Request.RequestContext.GetRoutedData<ContentVersion>();
            if (contentVersion == null)
                return HttpNotFound();

            MediaData mediaData = contentLoader.Get<MediaData>(contentVersion.ContentLink);
            if (mediaData == null)
                return HttpNotFound();

            FileStreamResult imageResult = new FileStreamResult(mediaData.BinaryData.OpenRead(), mediaData.MimeType);
            imageResult.FileDownloadName = mediaData.Name;

            return imageResult;
        }
    }
}
