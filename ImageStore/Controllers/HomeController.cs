using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageStore.Models;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;


namespace ImageStore.Controllers
{

    public class HomeController : Controller
    {
        public int pageSize = 4;

        private ImageRepository repo = new ImageRepository();
        [Authorize]
        public ActionResult MyList()
        {
            string us = "mylist";
            return RedirectToAction("List", "Home", new { userid = us });

        }

        public ViewResult List(int category = 0, int page = 1, string userid = "")
        {
            
            Category cat = repo.Catrgories.FirstOrDefault(x => x.CategoryID == category);
            ListViewModel<Image> model = new ListViewModel<Image>();
            int colitems = 0;
            if (userid == "")
            {
                model.Items = repo.Images
            .Where(p => (cat == null || p.CategoryID == cat.CategoryID) && !p.IsDeleted);
                colitems = model.Items.Count();
                model.Items = model.Items.OrderBy(im => im.DatePublishing)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
            }
            else
            {
                userid = User.Identity.GetUserId();
                model.Items = repo.Images
                .Where(p => ((cat == null || p.CategoryID == cat.CategoryID) && p.UserID == userid && !p.IsDeleted));
                colitems = model.Items.Count();
                model.Items = model.Items.OrderBy(im => im.DatePublishing)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            }
            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                UserID = userid,
                TotalItems = colitems

            };
            if (cat != null)
                model.CurrentCategory = cat.CategoryID;


            return View(model);
        }

        public FileContentResult GetPicture(int imageId)
        {
            Image im = repo.Images.FirstOrDefault(i => i.ImageID == imageId);

            if (im != null)
            {
                return File(im.Picture, im.PictureMimeType);
            }
            else
            {
                return null;
            }
        }

    }
}