using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBLite.Models
{
    public class ArticleReference
    {
        public string ArticleUrl { get; set; }
        public Uri ThumbnailUrl { get; set; }
        public string Excerpt { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
