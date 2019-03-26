using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class ArticleCollection
    {
        List<Article> articles;

        public List<Article> Articles { get => articles; set => articles = value; }
    }
}
