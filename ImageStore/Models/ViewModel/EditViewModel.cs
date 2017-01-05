using System.Collections.Generic;
using System.Web.Mvc;

namespace ImageStore.Models
{
    public class EditViewModel
    {
        public Image image { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}