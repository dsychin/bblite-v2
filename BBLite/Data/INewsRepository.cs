using BBLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBLite.Data
{
    interface INewsRepository
    {
        Task<IEnumerable<ArticleReference>> GetAll();
        Task<Article> GetArticle(string path);
    }
}
