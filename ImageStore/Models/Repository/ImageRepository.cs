using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageStore.Infrastructure;

namespace ImageStore.Models
{
    public class ImageRepository
    {
        ImageContext context = new ImageContext();

        public IEnumerable<Image> Images
        {
            get { return context.Images; }
        }
        public IEnumerable<Category> Catrgories
        {
            get { return context.Categories; }
        }
        public IEnumerable<Mark> Marks
        {
            get { return context.Marks; }
        }
        public IEnumerable<Country> Countries
        {
            get { return context.Countries; }
        }
        public IEnumerable<Comment> Comments
        {
            get { return context.Comments; }
        }
        public IEnumerable<AllActions> ActionsName
        {
            get { return context.UsersActions; }
        }
        public IEnumerable<Action> Actions
        {
            get { return context.Actions; }
        }

        public void SaveImage(Image image)
        {
            Image im = new Image();
            if (image.ImageID == 0)
               context.Images.Add(image);
            else
            {
                Image dbEntry = context.Images.Find(image.ImageID);
                if (dbEntry != null)
                {
                    dbEntry.Name = image.Name;
                    dbEntry.Comment= image.Comment;
                    dbEntry.CategoryID = image.CategoryID;
                    dbEntry.CountryID = image.CountryID;
                    dbEntry.DatePublishing = image.DatePublishing;
                    
                    dbEntry.IsDeleted = image.IsDeleted;
                    dbEntry.Latitude = image.Latitude;
                    dbEntry.Longitude = image.Longitude;
                    dbEntry.Picture = image.Picture;
                    dbEntry.Rating = image.Rating;
                    dbEntry.UserID = image.UserID;
                    dbEntry.PictureMimeType = image.PictureMimeType;
                }
            }
            context.SaveChanges();
        }
        public void SaveMark(Mark mark)
        {
            if (mark.MarkID == 0)
                context.Marks.Add(mark);
            else
            {
                Mark dbEntry = context.Marks.Find(mark.MarkID);
                if (dbEntry != null)
                {
                    dbEntry.ImageID = mark.ImageID;
                    
                    dbEntry.point = mark.point;
                    dbEntry.UserID = mark.UserID;
                }
            }
            context.SaveChanges();
        }
        public void SaveCountry(Country c)
        {
            if (c.CountryID == 0)
                context.Countries.Add(c);
            else
            {
                Country dbEntry = context.Countries.Find(c.CountryID);
                if (dbEntry != null)
                {
                    
                    dbEntry.Name = c.Name;
                }
            }
            context.SaveChanges();
        }
        public void SaveComment(Comment c)
        {
            if (c.CommentID == 0)
                context.Comments.Add(c);
            else
            {
                Comment dbEntry = context.Comments.Find(c.CommentID);
                if (dbEntry != null)
                {
                    dbEntry.ImageID = c.ImageID;
                    dbEntry.IsDeleted = c.IsDeleted;
                    dbEntry.Text = c.Text;
                    dbEntry.UserID = c.UserID;
                }
            }
            context.SaveChanges();
        }
        public void SaveCategory(Category c)
        {
            if (c.CategoryID == 0)
                context.Categories.Add(c);
            else
            {
                Category dbEntry = context.Categories.Find(c.CategoryID);
                if (dbEntry != null)
                {
                    dbEntry.Name = c.Name;
                }
            }
            context.SaveChanges();
        }
        public void SaveAllAction(AllActions c)
        {
            if (c.AllActionsID == 0)
                context.UsersActions.Add(c);
            else
            {
                AllActions dbEntry = context.UsersActions.Find(c.AllActionsID);
                if (dbEntry != null)
                {
                    dbEntry.ImageID = c.ImageID;
                    dbEntry.ActionID = c.ActionID;
                    dbEntry.UserID = c.UserID;
                }
            }
            context.SaveChanges();
        }
        public void SaveAllAction(Action c)
        {
            if (c.ActionID == 0)
                context.Actions.Add(c);
            else
            {
                Action dbEntry = context.Actions.Find(c.ActionID);
                if (dbEntry != null)
                {
                    dbEntry.Name = c.Name;
                }
            }
            context.SaveChanges();
        }


    }
}