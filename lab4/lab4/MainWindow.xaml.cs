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

        List<UserControlGeneral> newsCategoryGeneral = new List<UserControlGeneral>();
        List<UserControlGeneral> newsCategoryTechnology = new List<UserControlGeneral>();
        List<UserControlGeneral> newsCategoryBusiness = new List<UserControlGeneral>();
        List<UserControlGeneral> newsCategoryScience = new List<UserControlGeneral>();
        List<UserControlGeneral> newsCategoryEntertainment = new List<UserControlGeneral>();
        List<UserControlGeneral> newsCategoryHealth = new List<UserControlGeneral>();
        List<UserControlGeneral> newsCategorySports = new List<UserControlGeneral>();
        UserControlGeneral newsCategoryEmpty = new UserControlGeneral();
        


        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MainGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private async void GenerateArticlePage(List<UserControlGeneral> articleList)
        {
            contentGrid.Children.Clear();
            if (articleList.Count != 0)
            {
                int rowsNumber = (articleList.Count() + 1) / 2;
                while (rowsNumber != 0)
                {
                    contentGrid.RowDefinitions.Add(new RowDefinition());
                    rowsNumber--;
                }
                int i = 0;
                int j = 0;
                foreach (var article in articleList)
                {
                    Grid.SetColumn(article, i);
                    Grid.SetRow(article, j);
                    contentGrid.Children.Add(article);

                    if (i == 0)
                        i++;
                    else
                    {
                        i--;
                        j++;
                    }
                }
            }
            else
                contentGrid.Children.Add(newsCategoryEmpty);
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            int index = listViewMenu.SelectedIndex;
            MoveMenuPointer(index);

            contentGrid.ColumnDefinitions.Clear();
            contentGrid.RowDefinitions.Clear();
            contentGrid.ColumnDefinitions.Add(new ColumnDefinition());
            contentGrid.ColumnDefinitions.Add(new ColumnDefinition());

            switch (index)
            {
                case 0:
                    GenerateArticlePage(newsCategoryGeneral);
                    break;
                case 1:
                    GenerateArticlePage(newsCategoryTechnology);
                    break;
                case 2:
                    GenerateArticlePage(newsCategoryBusiness);
                    break;
                case 3:
                    GenerateArticlePage(newsCategoryScience);
                    break;
                case 4:
                    GenerateArticlePage(newsCategoryEntertainment);
                    break;
                case 5:
                    GenerateArticlePage(newsCategoryHealth);
                    break;
                case 6:
                    GenerateArticlePage(newsCategorySports);
                    break;
                default:
                    break;
            }
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

        private async void LoadArticle (ArticleCollection newsCollection, List<UserControlGeneral> articlePage)
        {
            articlePage.Clear();
            foreach (var article in newsCollection.Articles)
            {
                UserControlGeneral listElement = new UserControlGeneral();
                listElement.newsTitle.Text = article.Title;
                listElement.newsDescription.Text = article.Description;
                listElement.newsLink.Text = article.Source.Name;
                listElement.UrlToWebsite = article.Url;
                if (article.UrlToImage != null)
                {
                    try
                    {
                        listElement.newsImage.Source = new BitmapImage(new Uri(article.UrlToImage));
                    }
                    catch (UriFormatException e)
                    {

                    }
                }
                articlePage.Add(listElement);
            }
        }

        private async void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            string loadingBeginning = "Loading: ";
            string loadingEnding = " news...";

            try
            {
                string jsonResponse;
                ArticleCollection newsCollection;
                //general
                loadingInformationText.Text = loadingBeginning + "general" + loadingEnding;
                loadingProgressBar.Value = (int)Math.Round((float)0 * 100.0 / 7.0);
                jsonResponse = await NewsApiConnection.LoadDataAsync("general");
                newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                LoadArticle(newsCollection, newsCategoryGeneral);
                
                if (listViewMenu.SelectedIndex == 0)
                    GenerateArticlePage(newsCategoryGeneral);
                //newsCategoryGeneral.newsTitle.Text = newsCollection.Articles[1].Title;
                //newsCategoryGeneral.newsDescription.Text = newsCollection.Articles[1].Description;
                //newsCategoryGeneral.newsLink.Text = newsCollection.Articles[1].Source.Name;
                //if (newsCollection.Articles[1].UrlToImage != null)
                //    newsCategoryGeneral.newsImage.Source = new BitmapImage(new Uri(newsCollection.Articles[1].UrlToImage));

                //technology
                loadingInformationText.Text = loadingBeginning + "technology" + loadingEnding;
                loadingProgressBar.Value = (int)Math.Round((float)1 * 100.0 / 7.0);
                jsonResponse = await NewsApiConnection.LoadDataAsync("technology");
                newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                LoadArticle(newsCollection, newsCategoryTechnology);

                if (listViewMenu.SelectedIndex == 1)
                    GenerateArticlePage(newsCategoryTechnology);

                //business
                loadingInformationText.Text = loadingBeginning + "business" + loadingEnding;
                loadingProgressBar.Value = (int)Math.Round((float)2 * 100.0 / 7.0);
                jsonResponse = await NewsApiConnection.LoadDataAsync("business");
                newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                LoadArticle(newsCollection, newsCategoryBusiness);

                if (listViewMenu.SelectedIndex == 2)
                    GenerateArticlePage(newsCategoryBusiness);

                //science
                loadingInformationText.Text = loadingBeginning + "science" + loadingEnding;
                loadingProgressBar.Value = (int)Math.Round((float)3 * 100.0 / 7.0);
                jsonResponse = await NewsApiConnection.LoadDataAsync("science");
                newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                LoadArticle(newsCollection, newsCategoryScience);

                if (listViewMenu.SelectedIndex == 3)
                    GenerateArticlePage(newsCategoryScience);

                //entertainment
                loadingInformationText.Text = loadingBeginning + "entertainment" + loadingEnding;
                loadingProgressBar.Value = (int)Math.Round((float)4 * 100.0 / 7.0);
                jsonResponse = await NewsApiConnection.LoadDataAsync("entertainment");
                newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                LoadArticle(newsCollection, newsCategoryEntertainment);

                if (listViewMenu.SelectedIndex == 4)
                    GenerateArticlePage(newsCategoryEntertainment);

                //health
                loadingInformationText.Text = loadingBeginning + "health" + loadingEnding;
                loadingProgressBar.Value = (int)Math.Round((float)5 * 100.0 / 7.0);
                jsonResponse = await NewsApiConnection.LoadDataAsync("health");
                newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                LoadArticle(newsCollection, newsCategoryHealth);

                if (listViewMenu.SelectedIndex == 5)
                    GenerateArticlePage(newsCategoryHealth);

                //sports
                loadingInformationText.Text = loadingBeginning + "sports" + loadingEnding;
                loadingProgressBar.Value = (int)Math.Round((float)6 * 100.0 / 7.0);
                jsonResponse = await NewsApiConnection.LoadDataAsync("sports");
                newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                LoadArticle(newsCollection, newsCategorySports);

                if (listViewMenu.SelectedIndex == 6)
                    GenerateArticlePage(newsCategorySports);
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
                loadingProgressBar.Value = (int)Math.Round((float)7 * 100.0 / 7.0);
            }
        }
    }
}
