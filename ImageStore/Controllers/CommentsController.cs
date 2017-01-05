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
    [Authorize]
    public class CommentsController : Controller
    {

        ImageRepository repo = new ImageRepository();
        public int pageSize = 5;

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
        [AllowAnonymous]
        public ActionResult List(int ImageID, int page = 1)
        {
            
            ListViewModel<CommentView> model = new ListViewModel<CommentView>();

            IEnumerable<Comment> comments = repo.Comments
            .Where(p => p.ImageID == ImageID && p.IsDeleted == false)
            .OrderBy(im => im.DatePublishing)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
            List<CommentView> lc = new List<CommentView>();

            foreach (Comment com in comments)
            {
                CommentView cv = new CommentView();
                cv.Text = com.Text;
                if (UserManager.FindById(com.UserID) != null)
                    cv.UserName = UserManager.FindById(com.UserID).UserName;
                else
                    cv.UserName = "Гость";
                cv.CommentID = com.CommentID;
                cv.DatePubishing = com.DatePublishing;
                cv.UserID = com.UserID;
                lc.Add(cv);
            }
            model.Items = lc;

            model.image = repo.Images.FirstOrDefault(x => x.ImageID == ImageID);

            model.PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = model.Items.Count()
            };
            return PartialView(model);
        }

        
        [HttpPost]
        public ActionResult Delete(int CommentID, int page = 1)
        {
            
            Comment com = repo.Comments.FirstOrDefault(x => x.CommentID == CommentID);
            com.IsDeleted = true;
            repo.SaveComment(com);
            return RedirectToAction("Index","Image", new { imageID = com.ImageID });
        }

       
        public ActionResult AddComment(int imageid)
        {
            Comment model = new Comment();
            model.ImageID = imageid;
            model.Text = " ";
            return PartialView(model);
        }

     
        [HttpPost]
        public ActionResult AddComment(int imageid, string text)
        {
            string userid = User.Identity.GetUserId();
            repo.SaveComment(new Comment { UserID = userid, ImageID = imageid,
                DatePublishing = DateTime.Now, IsDeleted = false, Text = text});

            return RedirectToAction("Index", "Image", new { imageID = imageid });
        }
    }
}