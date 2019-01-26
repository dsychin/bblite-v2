using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBLite.Models;

namespace BBLite.Data
{
    public class NewsRepository : INewsRepository
    {
        public Task<IEnumerable<ArticleReference>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Article> GetArticle(string path)
        {
            throw new NotImplementedException();
        }
    }
}
