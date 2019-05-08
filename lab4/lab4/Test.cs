using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class Test
    {
        public List<Article> ArticleList { get; set; }

        public Test()
        {
            using (var db = new ArticleContext())
            {
                ArticleList = db.Articles.Where(x => x.Category == ArticleCategory.technology).ToList<Article>();
            }
        }
    }
}
