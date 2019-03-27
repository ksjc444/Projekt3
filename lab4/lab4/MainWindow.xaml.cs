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

        UserControlGeneral newsCategoryGeneral = new UserControlGeneral();
        UserControlGeneral newsCategoryTechnology = new UserControlGeneral();
        UserControlGeneral newsCategoryBusiness = new UserControlGeneral();
        UserControlGeneral newsCategoryScience = new UserControlGeneral();
        UserControlGeneral newsCategoryEntertainment = new UserControlGeneral();
        UserControlGeneral newsCategoryHealth = new UserControlGeneral();
        UserControlGeneral newsCategorySports = new UserControlGeneral();


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

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            int index = listViewMenu.SelectedIndex;
            MoveMenuPointer(index);

            switch (index)
            {
                case 0:
                    contentGrid.Children.Clear();
                    contentGrid.Children.Add(newsCategoryGeneral);
                    break;
                case 1:
                    contentGrid.Children.Clear();
                    contentGrid.Children.Add(newsCategoryTechnology);
                    break;
                case 2:
                    contentGrid.Children.Clear();
                    contentGrid.Children.Add(newsCategoryBusiness);
                    break;
                case 3:
                    contentGrid.Children.Clear();
                    contentGrid.Children.Add(newsCategoryScience);
                    break;
                case 4:
                    contentGrid.Children.Clear();
                    contentGrid.Children.Add(newsCategoryEntertainment);
                    break;
                case 5:
                    contentGrid.Children.Clear();
                    contentGrid.Children.Add(newsCategoryHealth);
                    break;
                case 6:
                    contentGrid.Children.Clear();
                    contentGrid.Children.Add(newsCategorySports);
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

        private async void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            string loadingBeginning = "Loading: ";
            string loadingEnding = " news...";
            try
            {
                loadingInformationText.Text = loadingBeginning + "general" + loadingEnding;
                loadingProgressBar.Value = (int)Math.Round((float)0 * 100.0 / 7.0);
                var jsonResponse = await NewsApiConnection.LoadDataAsync("general");
                var newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                newsCategoryGeneral.newsTitle.Text = newsCollection.Articles[1].Title;
                newsCategoryGeneral.newsDescription.Text = newsCollection.Articles[1].Description;
                newsCategoryGeneral.newsLink.Text = newsCollection.Articles[1].Source.Name;
                if (newsCollection.Articles[1].UrlToImage != null)
                    newsCategoryGeneral.newsImage.Source = new BitmapImage(new Uri(newsCollection.Articles[1].UrlToImage));
            }
            catch (ArgumentException ex) when (ex.ParamName == "category")
            {
                Console.WriteLine(ex.ParamName + "  -  " + ex.Message);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Prawdopodobnie brak połączenia z internetem:  " + ex.Message);
            }

            try
            {
                loadingInformationText.Text = loadingBeginning + "technology" + loadingEnding;
                loadingProgressBar.Value = (int)Math.Round((float)1 * 100.0 / 7.0);
                var jsonResponse = await NewsApiConnection.LoadDataAsync("technology");
                var newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                newsCategoryTechnology.newsTitle.Text = newsCollection.Articles[1].Title;
                newsCategoryTechnology.newsDescription.Text = newsCollection.Articles[1].Description;
                newsCategoryTechnology.newsLink.Text = newsCollection.Articles[1].Source.Name;
                if (newsCollection.Articles[1].UrlToImage != null)
                    newsCategoryTechnology.newsImage.Source = new BitmapImage(new Uri(newsCollection.Articles[1].UrlToImage));
            }
            catch (ArgumentException ex) when (ex.ParamName == "category")
            {
                Console.WriteLine(ex.ParamName + "  -  " + ex.Message);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Prawdopodobnie brak połączenia z internetem:  " + ex.Message);
            }

            try
            {
                loadingInformationText.Text = loadingBeginning + "business" + loadingEnding;
                loadingProgressBar.Value = (int)Math.Round((float)2 * 100.0 / 7.0);
                var jsonResponse = await NewsApiConnection.LoadDataAsync("business");
                var newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                newsCategoryBusiness.newsTitle.Text = newsCollection.Articles[1].Title;
                newsCategoryBusiness.newsDescription.Text = newsCollection.Articles[1].Description;
                newsCategoryBusiness.newsLink.Text = newsCollection.Articles[1].Source.Name;
                if (newsCollection.Articles[1].UrlToImage != null)
                    newsCategoryBusiness.newsImage.Source = new BitmapImage(new Uri(newsCollection.Articles[1].UrlToImage));
            }
            catch (ArgumentException ex) when (ex.ParamName == "category")
            {
                Console.WriteLine(ex.ParamName + "  -  " + ex.Message);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Prawdopodobnie brak połączenia z internetem:  " + ex.Message);
            }

            try
            {
                loadingInformationText.Text = loadingBeginning + "science" + loadingEnding;
                loadingProgressBar.Value = (int)Math.Round((float)3 * 100.0 / 7.0);
                var jsonResponse = await NewsApiConnection.LoadDataAsync("science");
                var newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                newsCategoryScience.newsTitle.Text = newsCollection.Articles[1].Title;
                newsCategoryScience.newsDescription.Text = newsCollection.Articles[1].Description;
                newsCategoryScience.newsLink.Text = newsCollection.Articles[1].Source.Name;
                if (newsCollection.Articles[1].UrlToImage != null)
                    newsCategoryScience.newsImage.Source = new BitmapImage(new Uri(newsCollection.Articles[1].UrlToImage));
            }
            catch (ArgumentException ex) when (ex.ParamName == "category")
            {
                Console.WriteLine(ex.ParamName + "  -  " + ex.Message);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Prawdopodobnie brak połączenia z internetem:  " + ex.Message);
            }

            try
            {
                loadingInformationText.Text = loadingBeginning + "entertainment" + loadingEnding;
                loadingProgressBar.Value = (int)Math.Round((float)4 * 100.0 / 7.0);
                var jsonResponse = await NewsApiConnection.LoadDataAsync("entertainment");
                var newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                newsCategoryEntertainment.newsTitle.Text = newsCollection.Articles[1].Title;
                newsCategoryEntertainment.newsDescription.Text = newsCollection.Articles[1].Description;
                newsCategoryEntertainment.newsLink.Text = newsCollection.Articles[1].Source.Name;
                if (newsCollection.Articles[1].UrlToImage != null)
                    newsCategoryEntertainment.newsImage.Source = new BitmapImage(new Uri(newsCollection.Articles[1].UrlToImage));
            }
            catch (ArgumentException ex) when (ex.ParamName == "category")
            {
                Console.WriteLine(ex.ParamName + "  -  " + ex.Message);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Prawdopodobnie brak połączenia z internetem:  " + ex.Message);
            }

            try
            {
                loadingInformationText.Text = loadingBeginning + "health" + loadingEnding;
                loadingProgressBar.Value = (int)Math.Round((float)5 * 100.0 / 7.0);
                var jsonResponse = await NewsApiConnection.LoadDataAsync("health");
                var newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                newsCategoryHealth.newsTitle.Text = newsCollection.Articles[1].Title;
                newsCategoryHealth.newsDescription.Text = newsCollection.Articles[1].Description;
                newsCategoryHealth.newsLink.Text = newsCollection.Articles[1].Source.Name;
                if (newsCollection.Articles[1].UrlToImage != null)
                    newsCategoryHealth.newsImage.Source = new BitmapImage(new Uri(newsCollection.Articles[1].UrlToImage));
            }
            catch (ArgumentException ex) when (ex.ParamName == "category")
            {
                Console.WriteLine(ex.ParamName + "  -  " + ex.Message);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Prawdopodobnie brak połączenia z internetem:  " + ex.Message);
            }

            try
            {
                loadingInformationText.Text = loadingBeginning + "sports" + loadingEnding;
                loadingProgressBar.Value = (int)Math.Round((float)6 * 100.0 / 7.0);
                var jsonResponse = await NewsApiConnection.LoadDataAsync("sports");
                var newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);

                newsCategorySports.newsTitle.Text = newsCollection.Articles[1].Title;
                newsCategorySports.newsDescription.Text = newsCollection.Articles[1].Description;
                newsCategorySports.newsLink.Text = newsCollection.Articles[1].Source.Name;
                if (newsCollection.Articles[1].UrlToImage != null)
                    newsCategorySports.newsImage.Source = new BitmapImage(new Uri(newsCollection.Articles[1].UrlToImage));
            }
            catch (ArgumentException ex) when (ex.ParamName == "category")
            {
                Console.WriteLine(ex.ParamName + "  -  " + ex.Message);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Prawdopodobnie brak połączenia z internetem:  " + ex.Message);
            }

            loadingInformationText.Text = "Complete";
            loadingProgressBar.Value = (int)Math.Round((float)7 * 100.0 / 7.0);
        }
    }
}
