using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ImageStore.Models;


namespace ImageStore.Controllers
{
    public class WebController : ApiController
    {
        ImageRepository repo = new ImageRepository();

        public IEnumerable<Image> GetAllCategories()
        {
            return repo.Images.ToArray();
        }
    }
}
