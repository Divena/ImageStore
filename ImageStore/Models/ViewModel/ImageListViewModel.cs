using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageStore.Models
{
    public class ListViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int CurrentCategory { get; set; }
        public Image image { get;  set; }
    }
}