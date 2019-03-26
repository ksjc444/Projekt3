using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var jsonResponse = await NewsApiConnection.LoadDataAsync("business");

                var newsCollection = JsonConvert.DeserializeObject<ArticleCollection>(jsonResponse);
                testBlock.Text = "";
                foreach (var article in newsCollection.Articles)
                {
                    testBlock.Text += $"OPIS ({article.Source.Name}):\n";
                    testBlock.Text += article.Description + "\n\nZAWARTOŚĆ:\n";
                    testBlock.Text += article.Content;
                    testBlock.Text += "\n--------------------\n\n";
                }
            }
            catch(ArgumentException ex) when (ex.ParamName=="category")
            {

                testowy.Content = "blad: " + ex;
            }
        }

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
    }
}
