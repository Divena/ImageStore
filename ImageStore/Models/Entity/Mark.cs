using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageStore.Models
{
    public class Mark
    {
        public int MarkID { get; set; }
        public int ImageID { get; set; }
        public string UserID { get; set; }
        public int point { get; set; }
    }
}