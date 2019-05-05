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

        //List<UserControlGeneral> newsCategoryGeneral = new List<UserControlGeneral>();
        //List<UserControlGeneral> newsCategoryTechnology = new List<UserControlGeneral>();
        //List<UserControlGeneral> newsCategoryBusiness = new List<UserControlGeneral>();
        //List<UserControlGeneral> newsCategoryScience = new List<UserControlGeneral>();
        //List<UserControlGeneral> newsCategoryEntertainment = new List<UserControlGeneral>();
        //List<UserControlGeneral> newsCategoryHealth = new List<UserControlGeneral>();
        //List<UserControlGeneral> newsCategorySports = new List<UserControlGeneral>();
        UserControlGeneral newsCategoryEmpty = new UserControlGeneral();



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

        private async void GenerateArticlePage(List<UserControlGeneral> articleList)
        {
            contentGrid.Children.Clear();
            if (articleList.Count != 0)
            {
                //int rowsNumber = (articleList.Count() + 1) / 2;
                //while (rowsNumber != 0)
                //{
                //    contentGrid.RowDefinitions.Add(new RowDefinition());
                //    rowsNumber--;
                //}
                //int i = 0;
                //int j = 0;
                //foreach (var article in articleList)
                //{
                //    Grid.SetColumn(article, i);
                //    Grid.SetRow(article, j);
                //    contentGrid.Children.Add(article);

                //    if (i == 0)
                //        i++;
                //    else
                //    {
                //        i--;
                //        j++;
                //    }
                //}
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

            //contentGrid.ColumnDefinitions.Clear();
            //contentGrid.RowDefinitions.Clear();
            //contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            //contentGrid.ColumnDefinitions.Add(new ColumnDefinition());

            GenerateArticlePage(newsCategoryArray[index]);

            //switch (index)
            //{
            //    case 0:
            //        GenerateArticlePage(newsCategoryGeneral);
            //        break;
            //    case 1:
            //        GenerateArticlePage(newsCategoryTechnology);
            //        break;
            //    case 2:
            //        GenerateArticlePage(newsCategoryBusiness);
            //        break;
            //    case 3:
            //        GenerateArticlePage(newsCategoryScience);
            //        break;
            //    case 4:
            //        GenerateArticlePage(newsCategoryEntertainment);
            //        break;
            //    case 5:
            //        GenerateArticlePage(newsCategoryHealth);
            //        break;
            //    case 6:
            //        GenerateArticlePage(newsCategorySports);
            //        break;
            //    default:
            //        break;
            //}
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
                        //listElement.imageTooltip.Text = article.UrlToImage;
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
            string loadingBeginning = "Loading: ";
            string loadingEnding = " news...";

            //ResourceDictionary newRes = new ResourceDictionary();
            //newRes.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml", UriKind.RelativeOrAbsolute);
            //this.Resources.MergedDictionaries.Clear();
            //this.Resources.MergedDictionaries.Add(newRes);

            RemoveOlderFromDatabase(1);

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

                ////general
                //loadingInformationText.Text = loadingBeginning + "general" + loadingEnding;
                //loadingProgressBar.Value = (int)Math.Round(0.0 * 100.0 / 7.0);
                //jsonResponse = await NewsApiConnection.LoadDataAsync("general");
                //newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                //LoadArticle(newsCollection, newsCategoryGeneral, ArticleCategory.general);

                //if (listViewMenu.SelectedIndex == 0)
                //    GenerateArticlePage(newsCategoryGeneral);
                ////newsCategoryGeneral.newsTitle.Text = newsCollection.Articles[1].Title;
                ////newsCategoryGeneral.newsDescription.Text = newsCollection.Articles[1].Description;
                ////newsCategoryGeneral.newsLink.Text = newsCollection.Articles[1].Source.Name;
                ////if (newsCollection.Articles[1].UrlToImage != null)
                ////    newsCategoryGeneral.newsImage.Source = new BitmapImage(new Uri(newsCollection.Articles[1].UrlToImage));


                ////technology
                //loadingInformationText.Text = loadingBeginning + "technology" + loadingEnding;
                //loadingProgressBar.Value = (int)Math.Round(1.0 * 100.0 / 7.0);
                //jsonResponse = await NewsApiConnection.LoadDataAsync("technology");
                //newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                //LoadArticle(newsCollection, newsCategoryTechnology, ArticleCategory.technology);

                //if (listViewMenu.SelectedIndex == 1)
                //    GenerateArticlePage(newsCategoryTechnology);

                ////business
                //loadingInformationText.Text = loadingBeginning + "business" + loadingEnding;
                //loadingProgressBar.Value = (int)Math.Round(2.0 * 100.0 / 7.0);
                //jsonResponse = await NewsApiConnection.LoadDataAsync("business");
                //newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                //LoadArticle(newsCollection, newsCategoryBusiness, ArticleCategory.business);

                //if (listViewMenu.SelectedIndex == 2)
                //    GenerateArticlePage(newsCategoryBusiness);

                ////science
                //loadingInformationText.Text = loadingBeginning + "science" + loadingEnding;
                //loadingProgressBar.Value = (int)Math.Round(3.0 * 100.0 / 7.0);
                //jsonResponse = await NewsApiConnection.LoadDataAsync("science");
                //newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                //LoadArticle(newsCollection, newsCategoryScience, ArticleCategory.science);

                //if (listViewMenu.SelectedIndex == 3)
                //    GenerateArticlePage(newsCategoryScience);

                ////entertainment
                //loadingInformationText.Text = loadingBeginning + "entertainment" + loadingEnding;
                //loadingProgressBar.Value = (int)Math.Round(4.0 * 100.0 / 7.0);
                //jsonResponse = await NewsApiConnection.LoadDataAsync("entertainment");
                //newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                //LoadArticle(newsCollection, newsCategoryEntertainment, ArticleCategory.entertainment);

                //if (listViewMenu.SelectedIndex == 4)
                //    GenerateArticlePage(newsCategoryEntertainment);

                ////health
                //loadingInformationText.Text = loadingBeginning + "health" + loadingEnding;
                //loadingProgressBar.Value = (int)Math.Round(5.0 * 100.0 / 7.0);
                //jsonResponse = await NewsApiConnection.LoadDataAsync("health");
                //newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                //LoadArticle(newsCollection, newsCategoryHealth, ArticleCategory.health);

                //if (listViewMenu.SelectedIndex == 5)
                //    GenerateArticlePage(newsCategoryHealth);

                ////sports
                //loadingInformationText.Text = loadingBeginning + "sports" + loadingEnding;
                //loadingProgressBar.Value = (int)Math.Round(6.0 * 100.0 / 7.0);
                //jsonResponse = await NewsApiConnection.LoadDataAsync("sports");
                //newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                //LoadArticle(newsCollection, newsCategorySports, ArticleCategory.sports);

                //if (listViewMenu.SelectedIndex == 6)
                //    GenerateArticlePage(newsCategorySports);
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
