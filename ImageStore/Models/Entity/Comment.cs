using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageStore.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string UserID { get; set; }
        public int ImageID { get; set; }
        public string Text { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DatePublishing { get; set; }
    }
}