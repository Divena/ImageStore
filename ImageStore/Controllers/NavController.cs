using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageStore.Models;
using System.Collections.Generic;
using System.IO;
using ImageStore.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace ImageStore.Controllers
{
    [Authorize]
    public class NavController : Controller
    {
        private ImageRepository repo = new ImageRepository();

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        [AllowAnonymous]
        public PartialViewResult Menu(int category = 0, string userid = "")
        {
            ViewBag.currentuser = userid;
            ViewBag.SelectedCategory = category;
            List<Category> categories = new List<Category>();

            foreach(Category c in repo.Catrgories)
            {
                Category cat = new Category() { Name = c.Name, CategoryID=c.CategoryID };
                categories.Add(cat);
            }

            return PartialView(categories);
        }

        public PartialViewResult ImageMenu(int imageid)
        {
            Image im = repo.Images.FirstOrDefault(x => x.ImageID == imageid);
            return PartialView(im);
        }

        public ActionResult Delete(int imageid)
        {
            Image im = repo.Images.FirstOrDefault(x => x.ImageID == imageid);
            if (im != null)
            {
                im.IsDeleted = true;
            }
            repo.SaveImage(im);
            return RedirectToAction("List", "Home");
        }

        public ActionResult Create()
        {
            
            EditViewModel model = new EditViewModel();
            IEnumerable<Category> c = repo.Catrgories;
            model.Categories = from cat in c
                               select new SelectListItem { Text = cat.Name, Value = cat.CategoryID.ToString() };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase image = null)
        {
            Stream str;
            if (image != null)
            {
                Image im = new Image();
                im.PictureMimeType = image.ContentType;
                im.Picture = new byte[image.ContentLength];
                image.InputStream.Read(im.Picture, 0, image.ContentLength);
                image.InputStream.Position = 0;
                str = image.InputStream;
                EXIFHelper exif = new EXIFHelper(str);
                im.Latitude = exif.Latitude;
                im.Longitude = exif.Longitude;
                im.Comment = exif.Comment;
                im.DatePublishing = DateTime.Now;

                ImageRepository repo = new ImageRepository();
                Category c = repo.Catrgories.Last();
                im.CategoryID = c.CategoryID;
                im.UserID = User.Identity.GetUserId(); 
                repo.SaveImage(im);
            }
            return RedirectToAction("List", "Home");
        }

        public ActionResult Edit(int imageid)
        {
            Image im;
            if (imageid == 0)
                im = new Image();
            else
                im = repo.Images.FirstOrDefault(x => x.ImageID == imageid);
            EditViewModel model = new EditViewModel();
            model.image = im;
            IEnumerable<Category> c = repo.Catrgories;

            model.Categories = from cat in c
                           select new SelectListItem { Text = cat.Name, Value = cat.CategoryID.ToString() };

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel ev)
        {
            Image im = repo.Images.FirstOrDefault(x => x.ImageID == ev.image.ImageID);
            im.Name = ev.image.Name;
            im.CategoryID = ev.image.CategoryID;
            repo.SaveImage(im);
            return RedirectToAction("Index", "Image", new { imageID = im.ImageID });
        }
    }
}