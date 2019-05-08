using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class Article
    {
        //Source source;
        //string author;
        //string title;
        //string description;
        //string url;
        //string urlToImage;
        //string publishedAt;
        //string content;
        //bool favourite;

        public int ArticleID { get; set; }

        public Source Source { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }

        public ArticleCategory Category { get; set; }
        public bool Favourite { get; set; }

        public string SourceName
        {
            get
            {
                if (Source == null)
                    return null;
                return Source.Name;
            }
            set
            {
                Source.Name = value;
            }
        }
    }
}
