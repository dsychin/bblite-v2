﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBLite.Models
{
    public class Article
    {
        public Uri OriginalUrl { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
