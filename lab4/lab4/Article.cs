using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class Article
    {
        Source source;
        string author;
        string title;
        string description;
        string url;
        string urlToImage;
        string publishedAt;
        string content;

        public Source Source { get => source; set => source = value; }
        public string Author { get => author; set => author = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public string Url { get => url; set => url = value; }
        public string UrlToImage { get => urlToImage; set => urlToImage = value; }
        public string PublishedAt { get => publishedAt; set => publishedAt = value; }
        public string Content { get => content; set => content = value; }
    }
}
