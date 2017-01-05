using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ImageStore.Models
{
    public class Image
    {
        public int ImageID { get; set; }
        public byte[] Picture { get; set; }
        
        public DateTime DatePublishing { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [MaxLength(100)]
        public string Comment { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        public int CategoryID { get; set; }
        public double Rating { get; set; }
        public string UserID { get; set; }
        public bool IsDeleted { get; set; }
        public int CountryID { get; set; }
        public string PictureMimeType { get; set; }
    }
}