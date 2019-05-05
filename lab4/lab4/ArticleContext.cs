using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class ArticleContext :DbContext
    {
        public ArticleContext() :base("NewsViewer")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ArticleContext>());
        }


        public DbSet<Article> Articles { get; set; }
    }
}
