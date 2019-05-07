using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;

namespace lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        List<UserControlGeneral>[] newsCategoryArray = new List<UserControlGeneral>[]
        {
            new List<UserControlGeneral>(),
            new List<UserControlGeneral>(),
            new List<UserControlGeneral>(),
            new List<UserControlGeneral>(),
            new List<UserControlGeneral>(),
            new List<UserControlGeneral>(),
            new List<UserControlGeneral>(),
        };

        UserControlGeneral newsCategoryEmpty = new UserControlGeneral();

        UserControlSettingsPage settingsPage = new UserControlSettingsPage();

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenSettings();
        }

        private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {

            }
        }

        private void OpenSettings()
        {
            
            contentGrid.Children.Clear();
            contentGrid.Children.Add(settingsPage);
            //settingsDisplayGrid.Children.Add(settingsPage);
        }

        private async void GenerateArticlePage(List<UserControlGeneral> articleList)
        {
            contentGrid.Children.Clear();
            if (articleList.Count != 0)
            {
                contentGrid.Columns = 2;
                foreach (var article in articleList)
                {
                    contentGrid.Children.Add(article);
                }
            }
            else
                contentGrid.Children.Add(newsCategoryEmpty);
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int index = listViewMenu.SelectedIndex;
            MoveMenuPointer(index);

            GenerateArticlePage(newsCategoryArray[index]);
        }

        private void MoveMenuPointer(int index)
        {
            transitioningContentSlide.OnApplyTemplate();
            menuPointer.Margin = new Thickness(0, 145 + (60 * index), 0, 0);
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private async void LoadArticle(ArticleCollection newsCollection, List<UserControlGeneral> articlePage, ArticleCategory category)
        {
            articlePage.Clear();
            AddToDatabase(newsCollection, category);
            using (var db = new ArticleContext())
            {
                foreach (var article in db.Articles.Where(x => x.Category == category))
                {
                    UserControlGeneral listElement = new UserControlGeneral();
                    listElement.newsTitle.Text = article.Title;
                    listElement.newsDescription.Text = article.Description;
                    listElement.newsLink.Text = article.Source.Name;
                    listElement.UrlToWebsite = article.Url;
                    listElement.ArticleID = article.ArticleID;
                    listElement.IsFavourite = article.Favourite;
                    
                    if (article.UrlToImage != null)
                    {
                        while (article.UrlToImage[0] == '/')
                        {
                            article.UrlToImage = article.UrlToImage.Remove(0, 1);
                        }
                        try
                        {
                            listElement.newsImage.Source = new BitmapImage(new Uri(article.UrlToImage));
                        }
                        catch (UriFormatException e)
                        {
                            Console.WriteLine("Exception uri: " + e);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("Exception io: " + e);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Exception default: " + e);
                        }

                    }
                    articlePage.Add(listElement);
                }
            }
        }

        private static void AddToDatabase(ArticleCollection newsCollection, ArticleCategory category)
        {
            List<Article> newArticlesList = new List<Article>();

            using (var db = new ArticleContext())
            {
                foreach (var article in newsCollection.Articles)
                {
                    if (!db.Articles.Where(x => x.Category == category).Any(x => x.Url == article.Url))
                    {
                        article.Category = category;
                        newArticlesList.Add(article);
                    }
                }

                db.Articles.AddRange(newArticlesList);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Remove all entries from database that are older than specified number of days
        /// </summary>
        /// <param name="days"></param>
        private static void RemoveOlderFromDatabase(int days)
        {
            using (var db = new ArticleContext())
            {
                db.Articles.RemoveRange(db.Articles.Where(x => DbFunctions.DiffDays(x.PublishedAt, DateTime.Now) >= days));
                db.SaveChanges();
            }
        }

        private async void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            await ReloadNews();
        }

        private async Task ReloadNews()
        {
            string loadingBeginning = "Loading: ";
            string loadingEnding = " news...";

            //ResourceDictionary newRes = new ResourceDictionary();
            //newRes.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml", UriKind.RelativeOrAbsolute);
            //this.Resources.MergedDictionaries.Clear();
            //this.Resources.MergedDictionaries.Add(newRes);

            RemoveOlderFromDatabase(Properties.Settings.Default.RemoveNewsAfterInDays);

            try
            {
                string jsonResponse;
                ArticleCollection newsCollection;


                int progress = 0;
                foreach (var categoryName in Enum.GetNames(typeof(ArticleCategory)))
                {
                    loadingInformationText.Text = loadingBeginning + categoryName + loadingEnding;
                    loadingProgressBar.Value = (int)Math.Round(progress * 100.0 / 7.0);
                    jsonResponse = await NewsApiConnection.LoadDataAsync(categoryName);
                    newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                    ArticleCategory category = (ArticleCategory)Enum.Parse(typeof(ArticleCategory), categoryName);

                    LoadArticle(newsCollection, newsCategoryArray[(int)category], category);

                    if (listViewMenu.SelectedIndex == (int)category)
                        GenerateArticlePage(newsCategoryArray[(int)category]);

                    progress++;
                }
            }
            catch (ArgumentException ex) when (ex.ParamName == "category")
            {
                Console.WriteLine(ex.ParamName + "  -  " + ex.Message);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Prawdopodobnie brak połączenia z internetem:  " + ex.Message);
                MessageBox.Show("Nie mogliśmy pobrać artykułów z internetu. Sprawdź połączenie z internetem.", "Wystąpił problem",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                loadingInformationText.Text = "Complete";
                loadingProgressBar.Value = (int)Math.Round(7.0 * 100.0 / 7.0);
            }
        }

        private void ReloadButtonContextAll_Click(object sender, RoutedEventArgs e)
        {
            ReloadButton_Click(sender, e);
        }

        private void ReloadButtonContextCurrent_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
