using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageStore.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using ImageStore.Models;
using System.Threading.Tasks;

namespace ImageStore.Controllers
{

    public class ImageController : Controller
    {
        ImageRepository repo = new ImageRepository();
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }


        public ActionResult Index(int imageID, int page = 1)
        {
            ListViewModel<CommentView> lc = new ListViewModel<CommentView>();
            lc.image = repo.Images.FirstOrDefault(c => c.ImageID == imageID);
            lc.PagingInfo = new PagingInfo { CurrentPage = page };
            return View(lc);
        }

        public ActionResult gps(int ImageID)
        {
            Image im = repo.Images.FirstOrDefault(x => x.ImageID == ImageID);

            return View(im);
        }

    }
}