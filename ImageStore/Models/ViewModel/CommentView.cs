using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageStore.Models
{
    public class CommentView
    {
        public string Text { get; set; }
        public int CommentID { get; set; }
        public string UserName { get; set; }
        public DateTime DatePubishing { get;  set; }
        public string UserID { get; set; }
    }
}