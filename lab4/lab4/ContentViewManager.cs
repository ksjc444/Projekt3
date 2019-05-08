using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class ContentViewManager : INotifyPropertyChanged
    {

        Article mock = new Article()
        {
            Author = "Admin",
            Title = "Przykładowy artukuł",
            Description = "Tak wygląda przykładowy artykuł",
            PublishedAt = DateTime.Now,
            Category = ArticleCategory.general
        };


        public ContentViewManager()
        {
            ShowNewsFromCategory(ArticleCategory.general);
        }

        private List<Article> articleList = new List<Article>();
        private byte numberOfColumns = 2;

        public List<Article> ArticleList
        {
            get
            {
                return articleList;
            }
            set
            {
                if (articleList != value)
                {
                    articleList = value;
                    OnPropertyChanged();
                }
            }
        }


        public byte NumberOfColumns
        {
            get
            {
                return numberOfColumns;
            }
            set
            {
                if (numberOfColumns != value)
                {
                    if (value <= 255)
                        numberOfColumns = value;
                    OnPropertyChanged();
                }
            }
        }

        public void ShowNewsFromCategory(ArticleCategory category)
        {
            using (var db = new ArticleContext())
            {
                ArticleList = db.Articles.Where(x => x.Category == category && x.Country == Properties.Settings.Default.NewsSourceCountryCode).ToList();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
