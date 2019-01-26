using BBLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBLite.Data
{
    public interface INewsRepository
    {
        Task<List<ArticleReference>> GetAll(int page);
        Task<Article> GetArticle(string path);
    }
}
